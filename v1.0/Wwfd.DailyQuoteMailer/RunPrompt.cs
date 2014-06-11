using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using BLB.Common;

namespace WwfdDailyQuoteMailer
{
	enum Commands
	{
		RunMailer
	}

	class RunPrompt
	{
		ApplicationLog _log;

		public RunPrompt()
		{
			_log = new ApplicationLog("Wwfd Daily Quote Mailer", "Program", true);
			//_log.AutoEmailOnError = true;
			//_log.AutoEmailOnErrorAddress = "britton.beckham@gmail.com";
			_log.OutputToConsole = true;
			//_log.MailServer
		}
		
		public void ShowPrompt()
		{
			Console.WriteLine("\n");

			Console.WriteLine("What would you like to do?\n");
			Console.WriteLine(" [1] Assign Quotes for Future Dates");
			Console.WriteLine(" [2] Set a Quote for a Specific Date");
			Console.WriteLine(" [3] Run Automated Mailer");
			Console.WriteLine(" [4] Show Stats");
			Console.WriteLine(" [5] Send Quote of the Day to Email");
			Console.WriteLine(" [9] Exit");

			ConsoleKeyInfo input;
			bool validResponse = false;
			Console.Write("\nEnter selection: ");

			do
			{

				input = Console.ReadKey(false);

				switch (input.Key)
				{
					//generate random quotes
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						validResponse = true;
						Console.WriteLine("\n\nThis will assign quotes (not used in at least one year) to dates up to the date speicified.");
						Console.Write("Enter final date: ");
						string date = Console.ReadLine();

						AssignQuotes(DateTime.Parse(date));
						break;

					//set quote for date
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						validResponse = true;
						break;

					//run daily mailer
					case ConsoleKey.D3:
					case ConsoleKey.NumPad3:
						validResponse = true;
						Console.WriteLine("\n");

						RunMailer();
						break;

					//show stats
					case ConsoleKey.D4:
					case ConsoleKey.NumPad4:
						validResponse = true;
						ShowStats();
						break;

					//send sepecific quote of the day
					case ConsoleKey.D5:
					case ConsoleKey.NumPad5:
						validResponse = true;
						Console.WriteLine("\n");

						//gather input information
						Console.Write("Enter quote ID number: ");
						string quoteId = Console.ReadLine();
						Console.Write("Enter email address of user: ");
						string toAddress = Console.ReadLine();

						SendQuoteOfTheDay(int.Parse(quoteId), toAddress);
						break;

					//exit
					case ConsoleKey.Escape:
					case ConsoleKey.D9:
					case ConsoleKey.NumPad9:
						validResponse = true;
						System.Environment.Exit(0);
						break;

				}

			} while (!validResponse);

			//add three blank lines
			Console.WriteLine("\n\n");
		}

		public void RunAutomatedCommand(Commands command)
		{
			switch(command)
			{
				case Commands.RunMailer: 
					RunMailer();
					break;
			}
		}

		void AssignQuotes(DateTime finalDate)
		{
			_log.AppendEvent(ApplicationEventTypes.Information, "Creating QOTD list until " + finalDate.ToShortDateString());

			//pull the stats information from the db
			WwfdData.GetStatsRow stats = new WwfdDataTableAdapters.GetStatsTableAdapter().GetData().First();
			DateTime lastQuoteDate = stats.LastQOTD;

			TimeSpan span = finalDate - lastQuoteDate;

			WwfdDataTableAdapters.QOTDTableAdapter qotdAdapter = new WwfdDataTableAdapters.QOTDTableAdapter();
			for (int i = 1; i <= span.Days; i++)
			{
				Console.WriteLine("Selecting quote for: " + lastQuoteDate.AddDays(i).ToShortDateString());
				qotdAdapter.CreateRandomQOTD();
			}

			_log.AppendEvent(ApplicationEventTypes.Information, "finished.");

			Pause();
			ShowPrompt();
		}

		void RunMailer()
		{
			_log.AppendEvent(ApplicationEventTypes.Information, "Running daily mailer.");

			//get today's qotd
			WwfdData.QOTDRow qotd = new WwfdDataTableAdapters.QOTDTableAdapter().GetQOTDByDate(DateTime.Today.ToShortDateString()).First();
			if (qotd.Status)
			{
				_log.AppendEvent(ApplicationEventTypes.NonCriticalWarning, "Quotes have already been sent.");
				return;
			}

			//get all subscribers
			WwfdData.QOTDSubscribersDataTable subscribers = new WwfdDataTableAdapters.QOTDSubscribersTableAdapter().GetActiveSubscribers();

			foreach (WwfdData.QOTDSubscribersRow subscriber in subscribers.Rows)
			{
				_log.AppendEvent(ApplicationEventTypes.Information, "Sending quote to " + subscriber.SubscriberEmail);
				string emailBody = BuildQOTD(subscriber.SubscriberID, System.DateTime.Today, qotd.QuoteText, qotd.FullName, qotd.ReferenceInfo);
				SendEmail(subscriber.SubscriberEmail, "Quote of the Day " + System.DateTime.Today.ToString("D"), emailBody);
			}

			new WwfdDataTableAdapters.QOTDTableAdapter().SetQOTDStatus(DateTime.Today.Date, true);

			_log.AppendEvent(ApplicationEventTypes.Information, "finished.");
		}

		void ShowStats()
		{
			//pull the stats information from the db
			WwfdData.GetStatsRow stats = new WwfdDataTableAdapters.GetStatsTableAdapter().GetData().First();

			Console.WriteLine("\n");
			Console.WriteLine("Quotes: " + stats.QuoteCount);
			Console.WriteLine("Founders: " + stats.FounderCount);
			Console.WriteLine("Subscribers: " + stats.SubscriberCount);
			Console.WriteLine("Last QOTD: " + stats.LastQOTD.ToShortDateString());

			Pause();

			ShowPrompt();
		}

		void Pause()
		{
			Console.WriteLine("\nPress any key to continue");
			Console.ReadKey();
		}

		void SendQuoteOfTheDay(int quoteId, string emailAddress)
		{
			WwfdData.QOTDRow quote;

			//get quote
			try
			{
				quote = new WwfdDataTableAdapters.QOTDTableAdapter().GetQuote(quoteId).First();
				string body = BuildQOTD(emailAddress, quote.QOTDDate, quote.QuoteText, quote.FullName, quote.ReferenceInfo);
				string subject = "Quote of The Day " + quote.QOTDDate.ToString("D");

				try
				{
					SendEmail(emailAddress, subject, body);
					Console.WriteLine("Email has been sent successfully.");
				}
				catch (Exception ex)
				{
					Console.WriteLine("The following error occurred while trying to send: " + ex.Message);
				}

			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Sequence contains no elements"))
					Console.WriteLine("The quote id specified is not a valid Quote of the Day.");
			}

			Pause();
			ShowPrompt();
		}

		string BuildQOTD(string subscriberId, DateTime quoteDate, string quote, string founder, string source)
		{
			//replace template variables
			string emailBody = File.ReadAllText("QuoteTemplate.txt");
			emailBody = emailBody.Replace("[[Date]]", quoteDate.ToString("D"));
			emailBody = emailBody.Replace("[[Quote]]", "\"" + quote + "\"");
			emailBody = emailBody.Replace("[[Founder]]", founder);
			emailBody = emailBody.Replace("[[Source]]", source);
			emailBody = emailBody.Replace("[[SubscriberID]]", subscriberId);

			return emailBody;
		}

		void SendEmail(string emailAddress, string subject, string emailBody)
		{
			//build message object
			MailMessage mail = new MailMessage();
			mail.To.Add(emailAddress);
			mail.From = new MailAddress("quoteoftheday@whatwouldthefoundersdo.org", "WhatWouldTheFoundersDo?");
			//mail.IsBodyHtml = true;
			mail.Subject = subject;
			mail.Body = emailBody;
			mail.IsBodyHtml = true;
			
			//build credentials
			SmtpClient smtp = new SmtpClient("mail.whatwouldthefoundersdo.org");
			smtp.Credentials = new NetworkCredential("quoteoftheday@whatwouldthefoundersdo.org", ".Wwfd$");

			//send
			smtp.Send(mail);
		}
	}
}

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
	class Program
	{
		static void Main(string[] args)
		{
			RunPrompt rp = new RunPrompt();
			rp.ShowPrompt();			
		}
	}
}


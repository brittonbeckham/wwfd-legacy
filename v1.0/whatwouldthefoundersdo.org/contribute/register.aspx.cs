using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using BLB.Common.Web;
using BLB.Common.Extensions;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;

public partial class _Register : System.Web.UI.Page
{
    //this number is Guid encoded each time the page loads, which creates a unique guid to use for confirmation.
    Guid _confNumber = 17760921.ToGuid();
    ContributorsTableAdapter contributors = new ContributorsTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageVariablesCollection vars = new PageVariablesCollection();
        vars.Add(new RequestVariable("confirm", typeof(Guid), false, PostMethods.Get));
        vars.Add(new RequestVariable("u", typeof(Guid), false, PostMethods.Get));
        this.ValidateVariables(vars);

        try
        {
            if (vars["confirm"].HasValue)
            {
                //check to make sure this is a legit conf number
                Guid conf = (Guid)vars["confirm"].Value;
                if (17760921 != conf.ToInt())
                    throw new Exception();

                if (!vars["u"].HasValue)
                {
                    throw new Exception();
                }
                else
                {
                    Guid userId = (Guid)vars["u"].Value;
					CoreDataObjects.ContributorsRow row = contributors.GetById(userId)[0];
                    row.Active = true;
                    contributors.Update(row);
                    Response.Redirect(string.Format("~/thank-you.aspx?message={0}", 2.ToGuid()));
                }
            }
        }
        catch
        {
            lblMessage.Text = "An error occured because a required value was not passed.";
            RegistrationForm.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.Equals(txtPassword1.Text, txtPassword2.Text))
                throw new Exception("The passwords did not match.");

            //insert new registration record
            Guid userGuid = Guid.NewGuid();
            contributors.Insert(userGuid, 1, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword1.Text, false);

            //build and send an email to webmaster for notification
            MailMessage mm = new MailMessage();
            mm.To.Add("britton.beckham@gmail.com");
            mm.From = new MailAddress("webmaster@whatwouldthefoundersdo.org", "WWFD Website");
            mm.IsBodyHtml = false;
            mm.Subject = "New Registration";
            mm.Body = string.Format("From:\t{0} {1}\nEmail:\t{2}\n\nMessage:\n{3}", txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtMessage.Text);

            //define smtp and authentication credential
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.whatwouldthefoundersdo.org";
            smtp.Credentials = new NetworkCredential("webmaster@whatwouldthefoundersdo.org", "blb456");

            //send the mail
            smtp.Send(mm);

            //build and send a confirmation email to the user
            mm = new MailMessage();
            mm.To.Add(txtEmail.Text);
            mm.To.Add("britton.beckham@gmail.com");
            mm.From = new MailAddress("webmaster@whatwouldthefoundersdo.org", "WWFD Website");
            mm.IsBodyHtml = false;
            mm.Subject = "Registration Confirmation";
            
            //get the registration confirmation email text
            string text = File.ReadAllText(Server.MapPath("~/emails/RegistrationConfirmation.txt"));
            mm.Body = string.Format(text, txtFirstName.Text, _confNumber, userGuid);

            smtp.Send(mm);

            Response.Redirect(string.Format("~/thank-you.aspx?message={0}", 1.ToGuid()));
        }
        catch(Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WebApi.Entities;
using WebApi.Helpers;
// using MailKit.Net.Smtp;
// using MailKit.Security;
// using Microsoft.Extensions.Options;
// using MimeKit;
// using MimeKit.Text;

namespace WebApi.Repositories
{
    public class mainRepo
    {
        private DataContext db;
        public mainRepo(DataContext T)
        {
            db = T;
        }

        public List<JobRequest> getJobRequest()
        {
            var result = db.JobRequests.ToList();
            return result;
        }
        
        public object getTheJobRequest(int ID)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.JobRequests.Where(x => x.MainTableFKId == ID).ToList();
            return result;
        }

        public object getCategories(string type)
        {
            var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Name).Distinct();
            // var result = db.MainTable.Where(x => x.Type == type).ToList();
            return result;
        }

        public object getServiceT(string type)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            // var result = db.ServiceList.Where(x => x.TypeOfService == type).ToList();
            var result = db.ServiceList.Where(x => x.TypeOfService.Contains(type)).ToList();
            return result;
        }

        public object getUSerT(string type)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            // var result = db.ServiceList.Where(x => x.TypeOfService == type).ToList();
            var result = db.MainTable.Where(x => x.Name.Contains(type)).ToList();
            return result;
        }


        public List<ServiceList> getServiceType()
        {
            var result = db.ServiceList.ToList();
            return result;
        }

        //Get information 
        public object getInformationUserName(string UserName)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            //var result = db.MainTable.Where(x => x.UserName.Contains(UserName)).ToList();
            var result = db.MainTable.Where(x => x.UserName == UserName).ToList();
            return result;
        }


        public object getServiceListInformation(int ServiceListID)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.ServiceList.Where(x => x.Id == ServiceListID).ToList();
            return result;
        }

        public object getInformation(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.MainTable.Where(x => x.Id == UserIDnum).ToList();
            return result;
        }

        public object getInformationContactDetails(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.ContactDetailsTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        public object getInformationBusinessHours(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.BusinessHoursTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        public object getInformationPhotos(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.PhotosTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        public object getInformationWorkLocations(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.WorkLocationTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        public object getInformationUsers(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.UserTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        public object getInformationReviews(int UserIDnum)
        {
            //var result = db.MainTable.Where(x => x.Type == type).Select(o => o.Category).Distinct();
            var result = db.ReviewTable.Where(x => x.mainTableFKId == UserIDnum).ToList();
            return result;
        }

        // Get selected user info        
        public object getSelectedUserInfo(string TypeSelected, string CountrySelected, string ProvinceSelected, string CategorySelected)
        {
            var result = from main in db.MainTable
                         join workLocation in db.WorkLocationTable on main.Id equals workLocation.mainTableFKId
                         where main.Type == TypeSelected &&
                               main.Category.Contains(CategorySelected) &&
                               workLocation.workInCountry == CountrySelected &&
                               workLocation.province.Contains(ProvinceSelected)
                         select main;

            return result.ToList();
        }

        // Filter Search    

        //For Just Province
        public object getFilterUserInfoProvince(string TypeSelected, string CountrySelected, string ProvinceSelected)
        {
            var result = from main in db.MainTable
                         join workLocation in db.WorkLocationTable on main.Id equals workLocation.mainTableFKId
                         where main.Type == TypeSelected &&
                               workLocation.workInCountry == CountrySelected &&
                               workLocation.province.Contains(ProvinceSelected)
                         select main;

            return result.ToList();
        }

        //For Just Category
        public object getFilterUserInfoCategory(string TypeSelected, string CategorySelected)
        {
            var result = from main in db.MainTable
                         join workLocation in db.WorkLocationTable on main.Id equals workLocation.mainTableFKId
                         where main.Type == TypeSelected &&
                               main.Category.Contains(CategorySelected)
                         select main;

            return result.ToList();
        }

        //For All Filters
        public object getFilterUserInfo(string TypeSelected, string CountrySelected, string ProvinceSelected, string CategorySelected)
        {
            var result = from main in db.MainTable
                         join workLocation in db.WorkLocationTable on main.Id equals workLocation.mainTableFKId
                         where main.Type == TypeSelected &&
                               workLocation.workInCountry == CountrySelected &&
                               main.Category.Contains(CategorySelected) &&
                               workLocation.province.Contains(ProvinceSelected)
                         select main;

            return result.ToList();
        }

        // Name Search        
        public object getNameUserInfo(string NameOFSearch)
        {
            //var result = db.MainTable.Where(x => x.Name.Contains(NameOFSearch)).Select(o => o.Name).Distinct();
            var result = db.MainTable.Where(x => x.Name.Contains(NameOFSearch)).ToList();
            return result;
        }

        // Service Name Search        
        public object getServiceNameUserInfo(string ServiceNameOFSearch)
        {
            //var result = db.MainTable.Where(x => x.Name == NameOFSearch).ToList(); //&& x.Province == ProvinceSelected
            var result = db.ServiceList.Where(x => x.TypeOfService.Contains(ServiceNameOFSearch)).ToList(); //&& x.Province == ProvinceSelected
            return result;
        }

        //Adding Information
        public bool addTheJobRequest(JobRequest T)
        {
            try
            {
                db.JobRequests.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool addTheServiceType(ServiceList T)
        {
            try
            {
                db.ServiceList.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool addTheUserMain(MainTable T)
        {
            try
            {
                db.MainTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTheUserContactDetails(ContactDetailsTable T)
        {
            try
            {
                db.ContactDetailsTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTheUserBussinesH(BusinessHoursTable T)
        {
            try
            {
                db.BusinessHoursTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTheUserPhotos(PhotosTable T)
        {
            try
            {
                db.PhotosTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTheUserWorkLocations(WorkLocationTable T)
        {
            try
            {
                db.WorkLocationTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTheUserUsers(UserTable T)
        {
            try
            {
                db.UserTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool addTheUserReview(ReviewTable T)
        {
            try
            {
                db.ReviewTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Update Information 


        public bool ChangeJobRequest(JobRequest T)
        {
            try
            {
                db.JobRequests.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeServiceType(ServiceList T)
        {
            try
            {
                db.ServiceList.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsMain(MainTable T)
        {
            try
            {
                db.MainTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsContactDetails(ContactDetailsTable T)
        {
            try
            {
                db.ContactDetailsTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsBussinesH(BusinessHoursTable T)
        {
            try
            {
                db.BusinessHoursTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsPhotos(PhotosTable T)
        {
            try
            {
                db.PhotosTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool ChangeUserDetailsWorkLocations(WorkLocationTable T)
        {
            try
            {
                db.WorkLocationTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsUsers(UserTable T)
        {
            try
            {
                db.UserTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserDetailsReview(ReviewTable T)
        {
            try
            {
                db.ReviewTable.Update(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public object DeleteServiceListID(int ServiceListID)
        {
            try
            {
                db.ServiceList.Remove(db.ServiceList.FirstOrDefault(x => x.Id == ServiceListID));
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public object DeleteJobRequest(int ID)
        {
            try
            {
                db.JobRequests.Remove(db.JobRequests.FirstOrDefault(x => x.Id == ID));
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public object DeleteUserID(int UserId)
        {
            try
            {
                db.MainTable.Remove(db.MainTable.FirstOrDefault(x => x.Id == UserId));
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Send Email

        /*
         
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string subject = "Hello";
        static string emailToAddress = "waaroorkal heen jy wil stuur se email eddress";
        static string emailFromAddress = "die email waarvanaf jy wil stuur"; //Sender Email Address
        static string password = "jou email se password (m.a.w die "emailFromAddress" se email se password, die fisiese een waarmee jy in jou email inlog)"; //Sender Password  


        public bool SendEmail()
        {
            try
            {
                body = "sit hier text in";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }         
         */

        static string smtpAddress = "smtp.gmail.com"; //"smtp.gmail.com"; 
        static int portNumber = 587; // 25 or 587  or  465
        static bool enableSSL = true;
        static string emailToAddress = "atyourservicebusiness1@gmail.com";
        static string emailFromAddress = "atyourservicebusiness1@gmail.com"; //Sender Email Address
        static string password = "cgofepruvwymvnwg"; //Sender Password    // new password= cgofepruvwymvnwg // Login in to email = @Service123

        public bool SendEmail(string NameSurname, string UserContactInfo, string subject, string body)
        {
            try
            {
                // var email = new MimeMessage();
                // email.From.Add(MailboxAddress.Parse(emailFromAddress)); //UsersEmailForBody
                // email.To.Add(MailboxAddress.Parse(emailToAddress));//emailToAddress
                // email.Subject = subject; //subject
                // email.Body = new TextPart(TextFormat.Html) { Text = "<b>Name & Surname : </b>" + NameSurname + "<br>" + "<b>Contact Information : </b>" + UserContactInfo + "<br><br>" + "<b>Message : </b>" + "<br>" + body + "<br><br>" + "<h2><b>DO NOT REPLY TO THIS EMAIL... </b></h2>" }; // body

                // using var smtp = new SmtpClient();
                // smtp.Connect(smtpAddress, portNumber);
                // smtp.Authenticate(emailFromAddress, password); // breaks here
                // smtp.Send(email);
                // smtp.Disconnect(true);

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress); //UsersEmailForBody
                    mail.To.Add(emailToAddress);//emailToAddress
                    mail.Subject = subject; //subject
                    mail.Body = "<b>Name & Surname : </b>" + NameSurname + "<br>" + "<b>Contact Information : </b>" + UserContactInfo + "<br><br>" + "<b>Message : </b>" + "<br>" + body + "<br><br>" + "<h2><b>DO NOT REPLY TO THIS EMAIL... </b></h2>"; // body
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail); // breaks here because aws blocks port 587  or  465
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SendEmailToPerson(string NameSurname, string UserContactInfo, string subject, string body, string ToPerson)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress); //UsersEmailForBody
                    mail.To.Add(ToPerson);//emailToAddress
                    mail.Subject = subject; //subject
                    mail.Body = "<b>Name & Surname : </b>" + NameSurname + "<br>" + "<b>Contact Information : </b>" + UserContactInfo + "<br><br>" + "<b>Message : </b>" + "<br>" + body + "<br><br>" + "<h2><b>Email was sent through @Your Service App <br> DO NOT REPLY TO THIS EMAIL... </b></h2>"; // body
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}


﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public class mainRepo
    {
        private DataContext db;
        public mainRepo(DataContext T)
        {
            db = T;
        }

        // get Functions
        public object getInformation(int UserIDnum)
        {
            var result = db.MainTable.Where(x => x.Id == UserIDnum).ToList();
            return result;
        }


        public object getGroupChatInformation(int GroupID)
        {
            var result = db.GroupMessageTable
                .Where(x => x.group_id == GroupID)
                .Select(x => new {
                    x.Id,
                    x.message,
                    x.DateAdded,
                    SenderName = x.MainTable.Name,  // Assuming MainTable contains sender's name
                    GroupId = x.group_id
                })
                .OrderBy(x => x.DateAdded)
                .ToList();
            return result;
        }

        public object getGroupID(string groupName)
        {
            var result = db.SubjectsTable.Where(x => x.Code == groupName).ToList();
            return result;
        }

        public object getDepartment()
        {
            var result = db.DepartmentTable.ToList();
            return result;
        }

        public object getSubjects(string department)
        {
            var result = db.SubjectsTable.Where(x => x.Department.Contains(department)).ToList();
            return result;
        }

        public object getFAQ(string department)
        {
            if (department == "ALL")
            {
                var result = db.FAQTable.ToList();
                return result;
            }
            else
            {
                var result = db.FAQTable.Where(x => x.department.Contains(department)).ToList();
                return result;
            }
        }

        public object getGroups(string department)
        {
            var result = db.SubjectsTable.Where(x => x.Department.Contains(department)).ToList();
            return result;
        }

        // Put

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

        // Post
        public bool addnewChat(PersonalChatsTable T)
        {
            try
            {
                db.PersonalChatsTable.Add(T);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Send Email

        static string smtpAddress = "smtp.gmail.com"; //"smtp.gmail.com"; 
        static int portNumber = 587; // 25 or 587  or  465
        static bool enableSSL = true;
        static string emailToAddress = "atyourservicebusiness1@gmail.com";
        static string emailFromAddress = "atyourservicebusiness1@gmail.com"; //Sender Email Address
        static string password = "wdom xhze dbob pcfn"; //Sender Password  // Login in to email = @Service123


        public bool SendEmailToPerson(string subject, string body, string ToPerson, string attachmentBase64, string fileName)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress); //UsersEmailForBody
                    if (string.IsNullOrEmpty(ToPerson))
                    {
                        mail.To.Add(emailToAddress);
                    }
                    else
                    {
                        mail.To.Add(ToPerson);
                    }
                    mail.Subject = subject; //subject
                    mail.Body = body + "<br><br>" + "<h2><b>Email was sent through @Your Service App <br> DO NOT REPLY TO THIS EMAIL... </b></h2>"; // body
                    mail.IsBodyHtml = true;

                    // Add the attachment if provided
                    if (!string.IsNullOrEmpty(attachmentBase64))
                    {
                        byte[] attachmentData = Convert.FromBase64String(attachmentBase64);
                        mail.Attachments.Add(new Attachment(new MemoryStream(attachmentData), fileName));
                    }

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


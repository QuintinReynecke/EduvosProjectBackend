using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Helpers;
using System.Net;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class mainController : Controller
    {
        public mainRepo myRepo;

        public mainController(DataContext T)
        {
            myRepo = new mainRepo(T);
        }


        [HttpGet("getConnectionStatus/{*url}")]
        public IActionResult Validate(string url)
        {
            try
            {
                // Ensure that the URL is properly URL-decoded
                var decodedUrl = WebUtility.UrlDecode(url);

                var request = WebRequest.Create(decodedUrl);
                request.Method = "HEAD";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response?.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(response?.StatusCode);
                    }
                    else
                    {
                        return Ok(response?.StatusCode);
                    }
                }
            }
            catch (WebException ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet("getJobRequests")]
        public async Task<IActionResult> GetAllJobRequests()
        {
            try
            {
                var result = myRepo.getJobRequest();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getJobRequests/{id}")]
        public async Task<IActionResult> GetSpecificJobRequest(int id)
        {
            try
            {
                var result = myRepo.getTheJobRequest(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/{type}")]
        public async Task<IActionResult> GetAllCategories(string type)
        {
            try
            {
                var result = myRepo.getCategories(type);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get Spicific Service Type 
        [HttpGet("getServiceT/{type}")]
        public async Task<IActionResult> GetServiceT(string type)
        {
            try
            {
                var result = myRepo.getServiceT(type);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get Spicific Service Type 
        [HttpGet("getUserT/{type}")]
        public async Task<IActionResult> GetUserT(string type)
        {
            try
            {
                var result = myRepo.getUSerT(type);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // Get user info from main menu  getUSerT

        [HttpGet("getServiceList")]
        public async Task<IActionResult> GetAllServiceType()
        {
            try
            {
                var result = myRepo.getServiceType();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserName/{UserName}")]
        public async Task<IActionResult> GetUserNameInformation(string UserName)
        {
            try
            {
                var result = myRepo.getInformationUserName(UserName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        [HttpGet("getServiceListInfo/{ServiceListID}")]
        public async Task<IActionResult> GetServiceInformation(int ServiceListID)
        {
            try
            {
                var result = myRepo.getServiceListInformation(ServiceListID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfo/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformation(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformation(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfoContactDetails/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationContactDetails(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationContactDetails(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfoBusinessHours/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationBusinessHours(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationBusinessHours(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfoPhotos/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationPhotos(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationPhotos(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfoWorkLocations/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationWorkLocations(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationWorkLocations(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserInfoUsers/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationUsers(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationUsers(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserReviews/{UserIDnum}")]
        public async Task<IActionResult> GetUserInformationReviews(int UserIDnum)
        {
            try
            {
                var result = myRepo.getInformationReviews(UserIDnum);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //get info for Service page

        [HttpGet("get/{TypeSelected}/{CountrySelected}/{ProvinceSelected}/{CategorySelected}")]
        public async Task<IActionResult> GetSelectedUser (string TypeSelected, string CountrySelected, string ProvinceSelected, string CategorySelected)
        {
            try
            {
                var result = myRepo.getSelectedUserInfo(TypeSelected, CountrySelected,  ProvinceSelected, CategorySelected);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Filter Search

        //For Just Province
        [HttpGet("getFilterP/{TypeSelected}/{CountrySelected}/{ProvinceSelected}")]
        public async Task<IActionResult> GetFilterUserProvince(string TypeSelected, string CountrySelected, string ProvinceSelected)
        {
            try
            {
                var result = myRepo.getFilterUserInfoProvince(TypeSelected, CountrySelected, ProvinceSelected);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //For Just Category
        [HttpGet("getFilterC/{TypeSelected}/{CategorySelected}")]
        public async Task<IActionResult> GetFilterUserCategory(string TypeSelected, string CategorySelected)
        {
            try
            {
                var result = myRepo.getFilterUserInfoCategory(TypeSelected, CategorySelected);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //For All Filters
        [HttpGet("getFilterA/{TypeSelected}/{CountrySelected}/{ProvinceSelected}/{CategorySelected}")]
        public async Task<IActionResult> GetFilterUser(string TypeSelected, string CountrySelected, string ProvinceSelected, string CategorySelected)
        {
            try
            {
                var result = myRepo.getFilterUserInfo(TypeSelected, CountrySelected, ProvinceSelected, CategorySelected);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Name Search

        [HttpGet("getName/{NameOFSearch}")]
        public async Task<IActionResult> GetNameUser(string NameOFSearch)
        {
            try
            {
                var result = myRepo.getNameUserInfo(NameOFSearch);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Name Search

        [HttpGet("getServiceName/{ServiceNameOFSearch}")]
        public async Task<IActionResult> GetServiceNameUser(string ServiceNameOFSearch)
        {
            try
            {
                var result = myRepo.getServiceNameUserInfo(ServiceNameOFSearch);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Adding
        [HttpPost("CreateJobRequest")]
        public async Task<IActionResult> AddJobRequest([FromBody] JobRequest T)
        {
            try
            {
                bool result = myRepo.addTheJobRequest(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewServiceType")]
        public async Task<IActionResult> AddServiceType([FromBody] ServiceList T)
        {
            try
            {
                bool result = myRepo.addTheServiceType(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserMain")]
        public async Task<IActionResult> AddUserMain([FromBody] MainTable T)
        {
            try
            {
                bool result = myRepo.addTheUserMain(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserContactDetails")]
        public async Task<IActionResult> AddUserContactDetails([FromBody] ContactDetailsTable T)
        {
            try
            {
                bool result = myRepo.addTheUserContactDetails(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserBussinesH")]
        public async Task<IActionResult> AddUserBussinesH([FromBody] BusinessHoursTable T)
        {
            try
            {
                bool result = myRepo.addTheUserBussinesH(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserPhotos")]
        public async Task<IActionResult> AddUserPhotos([FromBody] PhotosTable T)
        {
            try
            {
                bool result = myRepo.addTheUserPhotos(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserWorkLocations")]
        public async Task<IActionResult> AddUserWorkLocations([FromBody] WorkLocationTable T)
        {
            try
            {
                bool result = myRepo.addTheUserWorkLocations(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserUsers")]
        public async Task<IActionResult> AddUserUsers([FromBody] UserTable T)
        {
            try
            {
                bool result = myRepo.addTheUserUsers(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("InsertNewUserReview")]
        public async Task<IActionResult> AddUserReview([FromBody] ReviewTable T)
        {
            try
            {
                bool result = myRepo.addTheUserReview(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //update  

        [HttpPut("Update/JobRequests")]
        public async Task<IActionResult> ChangeJobRequests([FromBody] JobRequest T)
        {
            try
            {
                bool result = myRepo.ChangeJobRequest(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UpdateNewServiceType")]
        public async Task<IActionResult> ChangeServiceTypes([FromBody] ServiceList T)
        {
            try
            {
                bool result = myRepo.ChangeServiceType(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsMain")]
        public async Task<IActionResult> ChangeUserMain([FromBody] MainTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsMain(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsContactDetails")]
        public async Task<IActionResult> ChangeUserContactDetails([FromBody] ContactDetailsTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsContactDetails(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsBussinesH")]
        public async Task<IActionResult> ChangeUserBussinesH([FromBody] BusinessHoursTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsBussinesH(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsPhotos")]
        public async Task<IActionResult> ChangeUserPhotos([FromBody] PhotosTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsPhotos(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpPut("Update/UserDetailsWorkLocations")]
        public async Task<IActionResult> ChangeUserWorkLocations([FromBody] WorkLocationTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsWorkLocations(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsUsers")]
        public async Task<IActionResult> ChangeUserUsers([FromBody] UserTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsUsers(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/UserDetailsReview")]
        public async Task<IActionResult> ChangeUserReview([FromBody] ReviewTable T)
        {
            try
            {
                bool result = myRepo.ChangeUserDetailsReview(T);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // Delete
        [HttpDelete("DeleteJobRequests/{ID}")]
        public async Task<ActionResult<MainTable>> DeleteJobRequests(int ID)
        {
            try
            {
                var result = myRepo.DeleteJobRequest(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteUser/{UserId}")]
        public async Task<ActionResult<MainTable>> DeleteUser(int UserId)
        {
            try
            {
                var result = myRepo.DeleteUserID(UserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteServiceListType/{ServiceListID}")]
        public async Task<ActionResult<ServiceList>> DeleteServiceListID(int ServiceListID)
        {
            try
            {
                var result = myRepo.DeleteServiceListID(ServiceListID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Send Email
        // For Contact us Page

        [HttpPost("sendMail/{NameSurname}/{UserContactInfo}/{subject}/{body}")]
        public async Task<IActionResult> send_the_mail(string NameSurname, string UserContactInfo, string subject, string body)
        {
            try
            {
                var result = myRepo.SendEmail(NameSurname, UserContactInfo, subject, body);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // For Contacting a person through the app

        [HttpPost("sendMailServicePage/{NameSurname}/{UserContactInfo}/{subject}/{body}/{ToPerson}")]
        public async Task<IActionResult> send_the_mail_To_Person(string NameSurname, string UserContactInfo, string subject, string body, string ToPerson)
        {
            try
            {
                var result = myRepo.SendEmailToPerson(NameSurname, UserContactInfo, subject, body, ToPerson);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

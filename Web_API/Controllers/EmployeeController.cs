using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Cryptography;
using Web_API.Contract;
using Web_API.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       
        private readonly IEmpRepository _empRepository;
        public EmployeeController(IEmpRepository empRepository)
        {
                _empRepository=empRepository;
        }


        // Method For All Patient Start
        [HttpGet]
        public IEnumerable<NewPatient> AllPatient()
        {

            var emp = _empRepository.GellAllPatient();
            return emp;
        }






        //  [HttpGet("{Email}", Name = "Email_Check")]

        //public IActionResult CheckEmail(string value)
        //{

        //  var emp = _empRepository.checkMail(value);
        // if (emp == true) { return Ok(emp); }
        //else { return BadRequest(); }


        //}



        // Method For All Patient End

        //<---------------------------------------------------------------------------->




        // Method for Check Patient Email is already Available or Not Start

        //[HttpPost("Create")]
        //public IActionResult Create(Employee value)
        //{
        //    // 

        //    bool checkEmail = _empRepository.checkMail(value.Email);
        //    if (!checkEmail)
        //    {
        //        _empRepository.Insert(value);
        //        return Ok();
        //    }
        //    else
        //    {
        //        //  return StatusCode(400,"Email Already exist");
        //        return BadRequest();

        //    }
        //}



        //[HttpGet("{SingleUser}/employee", Name = "GetSingleEmp")]
        //public IActionResult GetSingleEmployeeCheck(string _value)
        //{

        //    var value = _empRepository.GetSingleEmployee(_value);
        //    if (value != null)
        //    {
        //        return Ok(value);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        // Method for Check Patient Email is already Available or Not End


        //<---------------------------------------------------------------------------->

        // PUT api/<EmployeeController>/5

        [HttpPut, Route("Update")]
        public ActionResult Put(string email, NewPatient value)
        {
            var patient = _empRepository.Get_SinglePatient_Details(email);

            if (patient != null)
            {
                _empRepository.UpdatePatient(email,value);

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }


        [HttpDelete, Route("Delete")]
        public ActionResult Delete(string email)
        {

            var employee = _empRepository.Get_SinglePatient_Details(email);
            if (employee != null)
            {
                _empRepository.Delete(email);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //<---------------------------------------------------------------------------->

        // Method For Admin Login start

        //   [HttpPost("Login")]
        [HttpPost, Route("Login")]
        public ActionResult Login(string Email, string Password)
        {

            if (Email != "null" && Password != "null")
            {

                var o = _empRepository.Login(Email, Password);

                if (o != null)
                {

                    return Ok(o);
                    //if (usr.RememberMe)
                    //{
                    //    CookieOptions option = new CookieOptions();
                    //    option.Expires = DateTime.Now.AddMinutes(20);
                    //    Response.Cookies.Append("Id", usr.LoginName, option);
                    //    Response.Cookies.Append("password", usr.Password, option);
                    //}

                }
                else
                {
                    return StatusCode(400, "Wrong EmailId or Password");
                    // return BadRequest();
                }
            }
            else
            {
                return StatusCode(406, "Null value found");
            }

        }

        // Method For Admin Login End

        //<---------------------------------------------------------------------------->

        // Method For Register New Patient  Start

        [HttpPost, Route("Newpatient")]
        public ActionResult New_patient(NewPatient emp)
        {

            if (emp.PtEmail != "null" && emp.PtName != "null")
            {


                bool checkemail = _empRepository.New_Patient_EMailcheck(emp.PtEmail);
                //var o = _emprepository.login(email, password);

                if (!checkemail)
                {
                    _empRepository.New_Patient_Insert(emp);
                    return Ok(emp);
                }
                else
                {
                    return StatusCode(403, "email already exist");
                    // return badrequest();
                }
            }
            else
            {
                return StatusCode(406, "null value found");
            }


        }

        // Method For Register New Patient  End


        //<---------------------------------------------------------------------------->

        // Method For New Patient Take Appointment Start

        [HttpPost, Route("Appointment")]
        public ActionResult Appointment(Appointment value)
        {

            if (value.Appointment_Email != "null" && value.Appointment_Name != "null")
            {


                bool checkEmail = _empRepository.New_Patient_EMailcheck(value.Appointment_Email);
                //var o = _empRepository.Login(Email, Password);

                if (checkEmail)
                {
                    bool existing_Appointment = _empRepository.Check_Existing_Appointment(value.Appointment_Date);

                    if (existing_Appointment)
                    {
                        return StatusCode(405, " Appointment is already exist");
                         //return BadRequest(value.Appointment_Date);

                    }
                    else
                    {
                        _empRepository.New_Appointment(value);
                        return Ok(value);
                    }
                }
                else
                {
                     //return StatusCode(404, "It's New Patient please first register");
                   return BadRequest();
                }
            }
            else
            {
                return StatusCode(406, "Null value found");
            }


        }


        // Method For New Patient Take Appointment End

        //<---------------------------------------------------------------------------->



        //Method For Search Existing Patient Start

        [HttpGet, Route("SearchPatient")]

        public async Task<ActionResult<IEnumerable<Appointment>>> Search_Patient(string email)

        {

            var emp = _empRepository.SearchPatient(email);
            if (emp.Count >=1) 
            { 
                return Ok(emp); 
            }
            
            else { 
                return StatusCode(404, "value found");
            }
            //return emp;

        }

        //Method For Search Existing Patient End


        [HttpGet, Route("Single_Patient_Details")]
        public IActionResult Get_Single_Patient_Details_Check(string _email)
        {

            var value = _empRepository.Get_SinglePatient_Details(_email);
            if (value != null)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet, Route("Single_Appointment_Details")]
        public IActionResult Get_Single_Appointment_Details_Check(int _id)
        {

            var value = _empRepository.Get_SingleAppointment_Details(_id);
            if (value != null)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpDelete, Route("Appointment_Delete")]
        public ActionResult Appointment_Delete(int _id)
        {

            var employee = _empRepository.Get_SingleAppointment_Details(_id);
            if (employee != null)
            {
                _empRepository.Single_Appointment_Delete(_id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut, Route("Appointment_Update")]
        public ActionResult Appointment_Update(int _id, Appointment value)
        {
            var patient = _empRepository.Get_SingleAppointment_Details(_id);

            if (patient != null)
            {
                _empRepository.Update_Appointment(_id, value);

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }



    }



}

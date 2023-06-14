using System.ComponentModel.DataAnnotations;

namespace Web_API.Entities
{
    public class Employee
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Password1{ get; set; }


     //-------------   new_Patient_Details_start  ------------
        //public string PtName { get; set; }

        //public string PtEmail { get; set; }

        //public string PtPhone { get; set; }
        
        //public string PtAge { get; set; }

        //public string PtGender { get; set; }

        //public string PtAddress { get; set; }
        
        // -------------   new_Patient_Details_End  ------------
        
        //public bool RememberMe { get; set; }



    }
}

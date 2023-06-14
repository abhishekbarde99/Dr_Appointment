using Web_API.Entities;
namespace Web_API.Contract
{
    public interface IEmpRepository
    {


        public void Insert(Employee emp);
        public void UpdatePatient(string email,NewPatient emp);
        public void Delete(string email);

        public bool checkMail(string Email);

        public List<NewPatient> GellAllPatient();

        public Employee GetSingleEmployee(string email);

        public NewPatient Get_SinglePatient_Details(string email);

        public Appointment Get_SingleAppointment_Details(int _id);

        public void Single_Appointment_Delete(int _id);

        public void Update_Appointment(int id, Appointment appointment);

      //  public bool Check_Existing_Appointment(DateTime date);


        public Employee Login(string Email, string Pass);

        public void New_Patient_Insert(NewPatient emp);

        public bool New_Patient_EMailcheck(string email);

        public void New_Appointment(Appointment value);

        public bool Check_Existing_Appointment(DateTime date);
        
        public List<Appointment> SearchPatient(string email);


        





    }
}

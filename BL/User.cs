using projMaxPark.DAL;

namespace projMaxPark.BL
{
    public class User
    {
        int userId;
        string userEmail;
        string userPassword;
        string userName;
        string userLastName;
        string userCarNum;
        string userPhone;
        string notificationCode;
        bool isAdmin;
        bool isManager;

        public int UserId { get => userId; set => userId = value; }

        public string NotificationCode { get => notificationCode; set => notificationCode = value; }

        public string UserEmail { get => userEmail; set => userEmail = value; }
        public string UserPassword { get => userPassword; set => userPassword = value; }
        public string UserName { get => userName; set => userName = value; }
        public string UserLastName { get => userLastName; set => userLastName = value; }
        public string UserCarNum { get => userCarNum; set => userCarNum = value; }
        public string UserPhone { get => userPhone; set => userPhone = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsManager { get => isManager; set => isManager = value; }

        public User() { }

        public User(int userId, string userEmail, string userPassword, string userName, string userLastName, string userCarNum, string userPhone, bool isAdmin, bool isManager)
        {
            UserId = userId;
            UserEmail = userEmail;
            UserPassword = userPassword;
            UserName = userName;
            UserLastName = userLastName;
            UserCarNum = userCarNum;
            UserPhone = userPhone;
            IsAdmin = isAdmin;
            IsManager = isManager;
        }

        public Object Login()
        {
            DBservicesUser login = new DBservicesUser();
            return login.UserLogin(this);
        }

        public string GetPushTokenByUserId(int userId)
        {
            DBservicesUser servicesUser = new DBservicesUser();
            string notificationCode = servicesUser.GetUserNotificationCodeById(userId);

            return notificationCode;
        }


        public void SaveNotificationCode(int userId, string notificationCode)
        {
            DBservicesUser login = new DBservicesUser();
            login.SaveNotificationCode(userId, notificationCode);
        }

        public Object GetUserById(int userId)
        {
            DBservicesUser dBservices = new DBservicesUser();
            return dBservices.GetUserById(userId);
        }

        public List<Reservation> getReservationList(int userId)
        {
            DBservicesUser dbs = new DBservicesUser();
            return dbs.getMyReservationsList(userId);
        }

        public int UpdateUserDetails()
        {
            DBservicesUser dbService = new DBservicesUser();
            return dbService.UpdateUserDetails(this);
        }

    }
}

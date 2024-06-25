using projMaxPark.DAL;
using System.Text.Json;

namespace projMaxPark.BL
{
    public class Admin
    {

        public List<User> getAllUsers()
        {
            DBservicesAdmin dbs = new DBservicesAdmin();
            return dbs.getAllUsers();
        }

        public int insertUser(User user)
        {
            DBservicesAdmin dbs = new DBservicesAdmin();
            return dbs.InsertNewUser(user);
        }

       public int insertMark(string mark, string blockMark)
        {
            DBservicesAdmin dbs = new DBservicesAdmin();
            return dbs.InsertMark(mark, blockMark);
        }

        //algorithm-delete marks from tblReservations & tbl,Mark
        public int deleteParkingMarks()
        {
            DBservicesAdmin dbsDeleteResCol= new DBservicesAdmin();
            dbsDeleteResCol.deleteColResMarks();

            DBservicesAdmin dbsDeleteMarks = new DBservicesAdmin();
           return  dbsDeleteMarks.deleteAllMarks();
        }

        //public int deleteUser(int id)
        //{
        //    DBservicesAdmin dbs = new DBservicesAdmin();
        //    return dbs.deleteUser(id);
        //}

        //update User details
        public User updateUser(User user)
        {
            DBservicesAdmin dbs = new DBservicesAdmin();
            return dbs.updateUser(user);
        }
    }
}

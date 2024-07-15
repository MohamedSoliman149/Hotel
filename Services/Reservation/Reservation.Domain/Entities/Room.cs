namespace Reservation.Domain.Entities
{
    public class Room
    {
        #region Constructors
        public Room()
        {

        }

        public Room(string name, RoomType type, string address, string locationURL)
        {            
            SetName(name);
            SetType(type);
            SetAddress(address);
            SetLocationURL(locationURL);
            SetIsDeleted(false);
        }

        #endregion

        #region Public Properties
        public int Id { get; set; }
        public string Name { get;  set; }
        public RoomType RoomType { get;  set; }

        [Required]
        public string Address { get;  set; }
        public string LocationURL { get;  set; }

        public bool IsDeleted { get;  set; }

        public List<RoomBlocking> RoomBlocking { get; set; }




        #endregion

        #region Public Methods

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Room name can't be null or empty");
            }

            else if (name.Length > 200)
            {
                throw new ArgumentException(string.Format("You have exceeded room name max length, max length is {0}.", 200));

            }
            Name = name;
        }
   
        public void SetType(RoomType type)
        {
            RoomType = type;
        }
        public void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Room address can't be null or empty");
            }
            Address = address;
        }
        public void SetLocationURL(string locationURL)
        {
            LocationURL = locationURL;
        }
        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

        #endregion
    }
}

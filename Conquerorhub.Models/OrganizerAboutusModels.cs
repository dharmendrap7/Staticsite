using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class OrganizerAboutusModels
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Name of organization is required")]
        public string NameOfOrganization { get; set; }
        [Required(ErrorMessage = "Date of incorporation is required")]
        public Nullable<System.DateTime> StartedInTheYear { get; set; }
       
        public AddressDetailJson Address { get; set; }
        [Required(ErrorMessage = "Contact Number is required")]
        public Nullable<Int64> ContactNumber { get; set; }
        [Required(ErrorMessage = "Email Id is required")]
        [RegularExpression("/^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$/ ", ErrorMessage = "Please enter valid Email Id")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Fax is required")]
        public Nullable<long> Fax { get; set; }

        [Required(ErrorMessage ="Vision of the organization is required")]
        public string VisionofTheOrganization { get; set; }
        [Required(ErrorMessage = "Mission of the organization is required")]
        public string MissionOfTheOrganization { get; set; }
        [Required(ErrorMessage = "Lines about organization is required")]
        public string Selfdesctiption { get; set; }
      
        
    }

    public class AddressDetailJson : DetailsJsonBase
    {
        [Required(ErrorMessage = "Nationality is required")]
        [RegularExpression("/^[a-zA-Z]+$/ ", ErrorMessage = "Please enter Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "State of incorporation is required")]
        [RegularExpression("/^[a-zA-Z]+$/ ", ErrorMessage = "Please enter valid State of incorporation")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country of incorporation is required")]
        [RegularExpression("/^[a-zA-Z]+$/ ", ErrorMessage = "Please enter valid country name")]
        public string Country { get; set; }
        [Required(ErrorMessage ="City of incorporation is required")]
        [RegularExpression("/^[a-zA-Z]+$/ ", ErrorMessage = "Please enter valid city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip is required")]
       
        [RegularExpression("/^\\d{6}$/", ErrorMessage = "Please enter valid zip code")]
        public int zip { get; set; }
        [Required(ErrorMessage ="Complete address is required")]
       
        public string ProperAddress { get; set; }



    }
}

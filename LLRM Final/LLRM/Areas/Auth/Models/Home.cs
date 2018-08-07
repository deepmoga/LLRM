using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LLRM.Areas.Auth.Models
{
    public class Home
    {

    }
    public class slider
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }

    }
    public class News
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }

    }
    public class Campuslife
    {
        public int id{ get; set; }
        public string Image { get; set; }
    }
    public class Tour
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public string Image { get; set; }
    }
    public class Pages
    {
        public int id { get; set; }
        public string PageName { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
    }
    public class department
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Department Name")]
        public string name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }

    }
    public class infrastructure
    {
       
        public int id { get; set; }
        [Required]
       
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
       
    }
    public class infrastructureData
    {
        [Key]
        public int Infraid { get; set; }
        [Required]

        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
        public virtual ICollection<InfraGallery> InfraGalleries { get; set; }
    }
    public class InfraGallery
    {
        [Key]
        public int InfGalId { get; set; }
       
        public int Infraid { get; set; }

        public virtual infrastructureData Infrastructures { get; set; }
        public string Image { get; set; }
    }
    public class Album
    {
        [Key]
        public int albumid { get; set; }
        public string AlbumName { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }

    }
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }
        [DisplayName("Album")]
        public int Albumid { get; set; }

        public virtual Album Albums { get; set; }
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Image { get; set; }
    }
    public class Message
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Designation { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }

    }
    public class Contact
    {
        public int id { get; set; }
        [Required]
        public string StudentHelpline { get; set; }
        public string AddmissionContact { get; set; }
        public string OfficeContact { get; set; }
        public string AddmissionEmail { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
    public class Account
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
    public class Video
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public string Url { get; set; }
    
    }
    public class Announcement
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }

    }
    public class Category
    {
        [Key]
        public int catid { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [DisplayName("Category")]
        public int Catid { get; set; }
        public virtual Category Categories { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public virtual ICollection<CourseGallery> CourseGalleries { get; set; }
      
    }
    public class CourseGallery
    {
        [Key]
        public int CourseGalId { get; set; }

        public int CourseId { get; set; }

        public virtual Course Courses { get; set; }
        public string Image { get; set; }
    }
    public class departmentdata
    {
        [Key]
        public int Depid { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
        public virtual ICollection<DepGallery> DepGalleries { get; set; }

    }
    public class DepGallery
    {
        [Key]
        public int DepGalId { get; set; }

        public int Depid { get; set; }

        public virtual departmentdata DepartmentDatas { get; set; }
        public string Image { get; set; }
    }
}
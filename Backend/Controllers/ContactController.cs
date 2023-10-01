using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactController : ControllerBase



    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;
        }
        // GET all contacts
        [HttpGet]
        public IActionResult Get()
        {
            var contacts = _context.Contacts.Include(c => c.ContactCategory).Include(c => c.ContactSubCategory).ToList();

            return Ok(contacts);
        }
        // GET contact by id
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _context.Contacts.Include(c => c.ContactCategory).Include(c => c.ContactSubCategory).FirstOrDefault(c => c.ContactId == id);
            return Ok(contact);
        }
        // POST contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Password = contactDto.Password,
                CategoryId = contactDto.CategoryId,
                SubCategoryId = contactDto.SubCategoryId,
                PhoneNumber = contactDto.PhoneNumber,
                DateOfBirth = contactDto.DateOfBirth,
                ContactCategory = contactDto.ContactCategory,
                ContactSubCategory = contactDto.ContactSubCategory
            };
            // if there is already a ContactCategory with the same name, use that one
            var existingContactCategory = _context.ContactCategory.FirstOrDefault(c => c.CategoryId == contactDto.ContactCategory.CategoryId);
            if (existingContactCategory != null)
            {
                contact.ContactCategory = existingContactCategory;
            }
            // if there is already a ContactSubCategory with the same name, use that one
            var existingContactSubCategory = _context.ContactSubCategory.FirstOrDefault(c => c.SubCategoryId == contactDto.ContactSubCategory.SubCategoryId);
            if (existingContactSubCategory != null)
            {
                contact.ContactSubCategory = existingContactSubCategory;
            }
            //check if email is unique, if not, return error
            var existingContact = _context.Contacts.FirstOrDefault(c => c.Email == contactDto.Email);
            if (existingContact != null)
            {
                return BadRequest(new { message = "Email is already taken." });
            }
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
        // PUT contact
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Contact contactDto, int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.Email = contactDto.Email;
            contact.Password = contactDto.Password;
            contact.CategoryId = contactDto.CategoryId;
            contact.SubCategoryId = contactDto.SubCategoryId;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.DateOfBirth = contactDto.DateOfBirth;
            contact.ContactCategory = contactDto.ContactCategory;
            contact.ContactSubCategory = contactDto.ContactSubCategory;
            // if there is already a ContactCategory with the same name, use that one
            var existingContactCategory = _context.ContactCategory.FirstOrDefault(c => c.CategoryId == contactDto.ContactCategory.CategoryId);
            if (existingContactCategory != null)
            {
                contact.ContactCategory = existingContactCategory;
            }
            // if there is already a ContactSubCategory with the same name, use that one
            var existingContactSubCategory = _context.ContactSubCategory.FirstOrDefault(c => c.SubCategoryId == contactDto.ContactSubCategory.SubCategoryId);
            if (existingContactSubCategory != null)
            {
                contact.ContactSubCategory = existingContactSubCategory;
            }
            _context.SaveChanges();
            return Ok(contact);
        }
        // DELETE contact
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);
            if (contact == null)
            {
                return BadRequest(new { message = "Email or password is incorrect." });
            }
            return Ok(new { message = "Ok" });
        }
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
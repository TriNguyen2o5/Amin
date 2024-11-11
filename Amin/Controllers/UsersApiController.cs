using Microsoft.AspNetCore.Mvc;
using Amin.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly PatientManagementContext _context;

    public UserApiController(PatientManagementContext context)
    {
        _context = context;
    }

    // Lấy tất cả User
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    // Lấy User theo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}

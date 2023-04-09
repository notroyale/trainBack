using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitsou.API.CommonAPI;

[ApiController]
[Route("[controller]")]
[Authorize]
public class APIController : ControllerBase
{

}
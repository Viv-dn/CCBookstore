using CCBookstore.Interfaces;
using CCBookstore.Models;
using CCBookstore.Repositories;
using CCBookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CCBookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : Controller
    {
        private readonly IBillingRepository _repo;
        private readonly BillingService _service;
        private readonly HttpClient _client;

        public BillingController(IBillingRepository repo, HttpClient client, BillingService service)
        {
            _repo = repo;
            _client = client;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Billing>>> GetAllBillingsAsync()
        {
            var billings = await _repo.GetAllBillingsAsync();
            return Ok(billings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBillingsByIdAsync(int id)
        {
            var billing = await _repo.GetBillingsByIdAsync(id);
            return Ok(billing);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBillingAsync(Billing billing)
        {
            await _repo.CreateBillingAsync(billing);
            //NotifyBillingAdded(billing);
            _service.NotifyBillingAdded(billing);
            return Ok(billing);
        }
    }
}

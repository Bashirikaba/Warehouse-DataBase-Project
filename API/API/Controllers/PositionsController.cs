using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models.Tables;
using NHibernate.Linq;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PositionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var positions = await _unitOfWork.Query<Position>().ToListAsync();
        return Ok(positions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var position = await _unitOfWork.Query<Position>().FirstOrDefaultAsync(p => p.Id == id);
        if (position == null)
            return NotFound();
        return Ok(position);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Имя не может быть пустым.");

        var position = new Position { Name = name };

        _unitOfWork.Add(position);

        try
        {
            await _unitOfWork.CommitAsync();
        }
        catch (NHibernate.Exceptions.GenericADOException ex) when (ex.Message.Contains("duplicate key") || ex.InnerException?.Message.Contains("duplicate") == true)
        {
            return Conflict($"Должность с названием '{name}' уже существует.");
        }

        return CreatedAtAction(nameof(Get), new { id = position.Id }, position);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Имя не может быть пустым.");

        var existing = await _unitOfWork.Query<Position>().FirstOrDefaultAsync(p => p.Id == id);
        if (existing == null)
            return NotFound();

        existing.Name = name;

        _unitOfWork.Update(existing);

        try
        {
            await _unitOfWork.CommitAsync();
        }
        catch (NHibernate.Exceptions.GenericADOException ex) when (ex.Message.Contains("duplicate key") || ex.InnerException?.Message.Contains("duplicate") == true)
        {
            return Conflict($"Должность с названием '{name}' уже существует.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var position = await _unitOfWork.Query<Position>().FirstOrDefaultAsync(p => p.Id == id);
        if (position == null)
            return NotFound();

        _unitOfWork.Delete(position);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }
}
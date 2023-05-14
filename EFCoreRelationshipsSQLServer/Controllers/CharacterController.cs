using EFCoreRelationshipsSQLServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsSQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon) // add with weapon object
                .Include(c=>c.Skills)
                //.ThenInclude idk we might add something else
                .ToListAsync();

            return characters;
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(CreateCharacterDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) { return NotFound(); }

            var newCharacter = new Character 
            { 
                Name = request.Name, 
                RPGClass = request.
                RpgClass, User = user 
            };

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await Get(newCharacter.UserId);
        }

        //it's should be in weapon controller but tutorial author just lazy
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> CreateWeapon(WeaponDto request)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);
            if (character == null) { return NotFound(); }

            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }
        //should be it skills controller
        [HttpPost("skill")]
        public async Task<ActionResult<Character>> AddCharacterSkill(CharacterSkillDto request)
        {
            var character = await _context.Characters
                .Where(c=>c.Id==request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
            if (character == null) { return NotFound(); }

            var skill = await _context.Skills.FindAsync(request.SkillId);
            if (character == null) { return NotFound(); }

            character.Skills.Add(skill); //idk

            await _context.SaveChangesAsync();

            return character;
        }
    }
}

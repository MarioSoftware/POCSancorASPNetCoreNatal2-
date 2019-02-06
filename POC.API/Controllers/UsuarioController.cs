using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using POC.Service;
using POC.Service.Dto;

namespace POC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ServiceUsuario _service = null;

        //Falta inyectar el servicio atraves de un conteiner
        public UsuarioController() {
            _service = new ServiceUsuario();
        }


        [HttpGet("/api/UserController/Get/{id}")]
        public UsuarioDto Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost("/api/UserController/Save")]
        public int Save([FromBody] UsuarioDto usuario)
        {
            _service.AddNew(usuario);
            return usuario.Id;
        }

        [HttpPut("/api/UserController/Modify")]
        public void Modify(UsuarioDto usuario)
        {
            _service.Modify(usuario);
        }

        [HttpDelete("/api/UserController/Delete/{id}")]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpGet("/api/UserController/GetAll")]
        public List<UsuarioDto> GetAll()
        {
            return _service.GetAll();
        }

    }
}

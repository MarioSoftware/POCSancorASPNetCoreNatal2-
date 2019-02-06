using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC.Service;
using POC.Service.Dto;

namespace POC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private ServiceMenu _service = null;

        //Falta inyectar el servicio atraves de un conteiner
        public MenuController()
        {
            _service = new ServiceMenu();
        }

        [HttpGet("/api/MenuController/Get/{id}")]
        public MenuDto Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost("/api/MenuController/Save")]
        public int Save(MenuDto menu)
        {
            _service.AddNew(menu);
            return menu.id;
        }

        [HttpPut("/api/MenuController/Modify")]
        public void Modify(MenuDto menu)
        {
            _service.Modify(menu);
        }

        [HttpDelete("/api/MenuController/Delete/{id}")]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpGet("/api/MenuController/GetAll")]
        public List<MenuDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("/api/MenuController/GetMenuesRaiz")]
        public List<MenuDto> GetMenuesRaiz()
        {
            return _service.GetAll().FindAll(x => x.tieneCols == true);
        }

        [HttpGet("/api/MenuController/GetMenuPorUsuario/{idUsuario}")]
        public List<MenuDto> GetMenuPorUsuario(int idUsuario)
        {
            ServiceUsuario _srvUsuario = new ServiceUsuario();
            UsuarioDto usuario = _srvUsuario.Get(idUsuario);

            ServicePerfilMenu _srvPerfilMenu = new ServicePerfilMenu();
            List<PerfilMenuDto> listaPerfilMenu = _srvPerfilMenu.GetAll().FindAll(x => x.Perfil.Id == usuario.Perfil.Id);

            List<MenuDto> listaRETURN = new List<MenuDto>();
            if(listaPerfilMenu!= null && listaPerfilMenu.Any())
            {
                foreach (PerfilMenuDto iPerfilMenu in listaPerfilMenu)
                    listaRETURN.Add(_service.Get(iPerfilMenu.Menu.id));
            }

            return listaRETURN;
        }

        [HttpGet("/api/MenuController/GetMenuPorPerfil/{idPerfil}")]
        public List<MenuDto> GetMenuPorPerfil(int idPerfil)
        {
            ServicePerfilMenu _srvPerfilMenu = new ServicePerfilMenu();
            List<PerfilMenuDto> listaPerfilMenu = _srvPerfilMenu.GetAll().FindAll(x => x.Perfil.Id == idPerfil);

            List<MenuDto> listaRETURN = new List<MenuDto>();
            if (listaPerfilMenu != null && listaPerfilMenu.Any())
            {
                foreach (PerfilMenuDto iPerfilMenu in listaPerfilMenu)
                    listaRETURN.Add(_service.Get(iPerfilMenu.Menu.id));
            }

            return listaRETURN;
        }
    }
}
using Newtonsoft.Json;
using POC.Service.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POC.Service
{
    public class ServiceMenuItem
    {
        private string _path = @"./Archivos/MenuItem.json";

        public ServiceMenuItem()
        {
        }

        public int GetNextId()
        {
            List<MenuItemDto> listaMenuItem = this.GetAll();

            if (listaMenuItem != null && listaMenuItem.Any())
                return listaMenuItem.Max(x => x.id) + 1;
            else
                return 1;
        }

        public bool AddNew(MenuItemDto pMenuItem)
        {
            pMenuItem.id = this.GetNextId();
            Save(pMenuItem);

            return true;
        }

        public void Save(MenuItemDto pMenuItem)
        {
            string json = JsonConvert.SerializeObject(pMenuItem);

            if (File.Exists(_path))
            {
                using (var writer = new StreamWriter(_path, true))
                {
                    writer.WriteLine(json);
                    writer.Close();
                }
            }
            else
            {
                File.WriteAllText(_path, json);
                using (var writer = new StreamWriter(_path, true))
                {
                    writer.WriteLine(string.Empty);
                    writer.Close();
                }
            }
        }

        public void Save(List<MenuItemDto> listaMenuItem)
        {
            foreach (MenuItemDto iMenuItem in listaMenuItem)
                this.Save(iMenuItem);
        }

        public bool Modify(MenuItemDto pMenuItem)
        {
            List<MenuItemDto> listaMenuItem = this.GetAll();

            if (pMenuItem != null && listaMenuItem.FirstOrDefault(x => x.id == pMenuItem.id) != null)
            {
                listaMenuItem.FirstOrDefault(x => x.id == pMenuItem.id).name = pMenuItem.name;
                listaMenuItem.FirstOrDefault(x => x.id == pMenuItem.id).link = pMenuItem.link;
                listaMenuItem.FirstOrDefault(x => x.id == pMenuItem.id).isFavorite = pMenuItem.isFavorite;
                listaMenuItem.FirstOrDefault(x => x.id == pMenuItem.id).prefix = pMenuItem.prefix;

                File.Delete(_path);
                this.Save(listaMenuItem);

                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            List<MenuItemDto> listaMenuItem = this.GetAll();

            if (listaMenuItem != null && listaMenuItem.FirstOrDefault(x => x.id == id) != null)
            {
                listaMenuItem = listaMenuItem.FindAll(x => x.id != id);
                File.Delete(_path);
                this.Save(listaMenuItem);

                return true;
            }

            return false;
        }

        public List<MenuItemDto> GetAll()
        {
            List<MenuItemDto> listaMenuItem = new List<MenuItemDto>();
            if (File.Exists(_path))
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(_path);
                while ((line = file.ReadLine()) != null)
                {
                    var _menuItemDto = JsonConvert.DeserializeObject<MenuItemDto>(line);
                    listaMenuItem.Add(_menuItemDto);
                }

                file.Close();
            }
            return listaMenuItem;
        }

        public MenuItemDto Get(int id)
        {
            var listaMenuItem = this.GetAll();
            return listaMenuItem.FirstOrDefault(x => x.id == id);
        }
    }
}

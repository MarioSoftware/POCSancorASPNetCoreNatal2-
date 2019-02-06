var crudMixin = {
    methods: {

        Nuevo: function () {
            this.selection = null;
            vm.Component.find('nf-table')[0].deselectAll();

            this.selectedLocality = null;
            this.selectedProvince = null;
            this.selectedPais = null;
            this.provinciaSource = [];
            this.localidadSource = [];
            this.modal = {
                Id: 0,
                Nombre: '',
                Ciudad: {
                    Id: 0,
                    Nombre: '',
                    Provincia: {
                        Id: 0,
                        Nombre: '',
                        Pais: {
                            Id: 0,
                            Nombre: ''
                        }
                    }
                }
            };

            this.$refs.modal.open();
        },

        Detalle() { 

            this.$refs.detailModal.open();
        },

        Editar() { 

            if (this.selection !== null && this.selection !== undefined) {

                this.IsEditing = true;

                var selectedEntity = this.selection[0]; 

                this.selectedPais = { id: selectedEntity.Ciudad.Provincia.Pais.Id, text: selectedEntity.Ciudad.Provincia.Pais.Nombre};
               
                this.selectedProvince = { id: selectedEntity.Ciudad.Provincia.Id, text: selectedEntity.Ciudad.Provincia.Nombre };

                 this.selectedLocality = { id: selectedEntity.Ciudad.Id, text: selectedEntity.Ciudad.Nombre };

                this.$refs.modal.open();
 
            }

        },

        Eliminar() { 

            $.ajax({
                url: _endpoint.Usuario.Delete + this.selection[0].Id,
                    type: "DELETE",
                    contentType: "application/json;chartset=utf-8",
            })
                .done(function () {
                    var selectedIds = vm.$data.selection.map(function (user) {
                        return user.Id;
                    });

                    vm.$data.data = vm.$data.data.filter(function (user) {
                        return selectedIds.indexOf(user.Id) === -1;
                    });

                    vm.updateSelectedRow();
                });

               
            
        }

    }
}
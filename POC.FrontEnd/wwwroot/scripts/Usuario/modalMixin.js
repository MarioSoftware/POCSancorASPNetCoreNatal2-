var modalMixin = {

    mounted: function () {
        // Form validation espera callback globales
        window.checkPaisSelected = function () {
            return Boolean(this.selectedPais);
        }.bind(this);

        window.checkProvinceSelected = function () {
            return Boolean(this.selectedProvince);
        }.bind(this);

        window.checkLocalitySelected = function () {
            return Boolean(this.selectedLocality);
        }.bind(this);
    },

    watch: {
        selectedPais: function (newVal) {
            if (newVal === null)
                return;
            this.filtroPaisPorProvincia(newVal); 
            
            if (!this.IsEditing) { 
                this.selectedLocality = null;
                this.localidadSource = [];
            }

            if (vm.localidadSource != null && vm.localidadSource.length > 0)
            {
                this.IsEditing = true;
            }
            
            this.filtroPaisPorProvincia(newVal);
        },
        selectedProvince: function (newVal) {
            if (newVal === undefined || newVal === null) {
                this.localidadSource = [];
                return;
            }
            this.filtroProvinciaPorCiudad(newVal);
        }
    },

    methods: {

        filtroPaisPorProvincia(newVal) {
            $.ajax({
                type: "GET",
                dataType: "json",
                async: false,
                url: _endpoint.Provincia.GetByPais + newVal.id,
                success: function (data) {
                    vm.provinciaSource = vm.modificacionVariables(data);
                }
            });
        },

        filtroProvinciaPorCiudad(newVal) {
            $.ajax({
                type: "GET",
                dataType: "json",
                async: false,
                url: _endpoint.Ciudad.GetByProvincia + newVal.id,
                success: function (data) {
                    vm.localidadSource = vm.modificacionVariables(data);
                    vm.$forceUpdate();
                }
            });
        },

        formSubmition() {

            if (NF.Validation.Form.validate('#modalForm')) {
                if (this.selection[0] !== null && this.selection[0] !== undefined) {

                    var entityJson = JSON.stringify(vm.modal);
                    $.ajax({
                        url: 'https://localhost:44309/api/UserController/Modify/',
                        type: "PUT",
                        data: entityJson,
                        processData: true,
                        contentType: "application/json;chartset=utf-8",
                    })
                        .done(function () {

                            var rowIndex = vm.selection[0].nfuuid;  
                            if (rowIndex !== null && rowIndex !== undefined && vm.modal !== null && vm.modal !== undefined) {
                                vm.data[rowIndex].Nombre = vm.modal.Nombre;
                                vm.data[rowIndex].Ciudad = vm.modal.Ciudad;
                            } 

                        });

                } else {
                 
                    this.modal.Ciudad.Id = this.selectedLocality.id;
                    this.modal.Ciudad.Nombre = this.selectedLocality.text;

                    this.modal.Ciudad.Provincia.Id = this.selectedProvince.id;
                    this.modal.Ciudad.Provincia.Nombre = this.selectedProvince.text;

                    this.modal.Ciudad.Provincia.Pais.Id = this.selectedPais.id;
                    this.modal.Ciudad.Provincia.Pais.Nombre = this.selectedPais.text;


                    var dataJson = JSON.stringify(vm.modal);

                    $.ajax({
                        url: _endpoint.Usuario.Create,
                        type: "POST",
                        data: dataJson,
                        processData: true,
                        contentType: "application/json;chartset=utf-8",
                        success: function (data) {
                            console.log(data);
                        }
                    });

                }
                this.$refs.modal.close();
            }
        }


    }
}
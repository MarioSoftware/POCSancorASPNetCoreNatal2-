// Definición del View Model
var ViewModel = {
    el: '#userTemplate',
    lang: 'es-AR',
    data: {
        data: [],
        configColumns: configColumns,
        select: NF.Table.Select.SINGLE,
        toolbar: '#myToolbar',
        mode: NF.Table.Mode.FULL,
        responsive: false,
        selection: null,
        IsEditing:false,

        modal: {
            Id: -1,
            Nombre: '',
            Ciudad: {
                Id: -1,
                Nombre: "",
                Provincia: {
                    Id: -1,
                    Nombre: "",
                    Pais: {
                        Id: -1,
                        Nombre: ""
                    }
                }
            }
        },

        paisSource: [],
        provinciaSource: [],
        localidadSource: [],

        selectedLocality: null,
        selectedProvince: null,
        selectedPais: null
         

    },
    mixins: [modalMixin,crudMixin, tableMixin],
    watch: {

    },

    methods: {

        modificacionVariables(data) {
            var entitys = [];
            for (var i = 0; i < data.length; i++) {
                var entity = { id: 0, text: "" };
                entity.id = data[i].Id;
                entity.text = data[i].Nombre;
                entitys.push(entity);
            }
            return entitys;
        },
        cargarTabla(self) {
           
            $.ajax({
                type: "GET",
                dataType: "json",
                async: false,
                url: 'https://localhost:44309/api/UserController/GetAll',
                success: function (data) {
                    self.data = data;
                }
            });
        }
    },

    mounted: function () {
        var self = this;

        self.cargarTabla(self);

        $.ajax({
            type: "GET",
            dataType: "json",
            async: false,
            url: _endpoint.Pais.GetAll,
            success: function (data) {
                self.paisSource = self.modificacionVariables(data);
            }
        });
    }
};

// Instanciamos el View Model
var vm = new NF(ViewModel);
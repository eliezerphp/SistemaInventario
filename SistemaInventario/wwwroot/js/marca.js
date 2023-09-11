let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $("#tblDatos").DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por pagina",
            "zeroRecords": "Ningun registro",
            "info": "Mostrar pagina _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url" : "/Admin/Marca/ObtenerTodos"
        },
        "columns": [
            { "data": "nombre", "width": "20%" }, //con ancho definido y no se pase del 100% de la columna
            { "data": "descripcion", "width": "40%" },
            {
                "data": "estado", 
                "render": function (data) { //Funcion que dependiendo del estado, me imprima activo o inactivo en lugar de true o false
                    if (data == true) { //"data" vale "estado" dentro de esta funcion
                        return "Activo"
                    }
                    else {
                        return "Inactivo"
                    }
                }, "width" : "20%"
            },
            {
                "data": "id",
                "render": function (data) {//Data vale "Id"
                    // el simbolo de comilla invertida `` nos permite renderizar codigo HTML
                    return ` 
                        <div class="text-center">

                            <a href="/Admin/Marca/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>

                            <a Onclick=Delete("/Admin/Marca/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer")>
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `
                }, "width" : "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de eliminar la marca?",
        text: "Este registro no podra ser recuperado",
        icon: "error",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
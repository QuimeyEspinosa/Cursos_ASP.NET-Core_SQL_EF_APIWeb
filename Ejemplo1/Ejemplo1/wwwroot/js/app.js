const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');

const expresiones = {
    nombre: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    precio: /^[0-9]+([.])?([0-9]+)?$/
}

const campos = {
    descripcion: false,
    precio: false
}

const validarForm = (e) => {

    switch (e.target.name) {

        case "descripcion":
            validarCampo(expresiones.nombre, e.target, 'descripcion');
            break;

        case "precio":
            validarCampo(expresiones.precio, e.target, 'precio');
            break;
    }
}

const validarCampo = (expresion, input, campo) => {

    if (expresion.test(input.value)) {
        campos[campo] = true;
    }
    else {
        campos[campo] = false;
        alert("El campo " + campo + " es incorrecto");
    }
}

inputs.forEach((input) => {
    input.addEventListener('blur', validarForm); //submit ejecuta la función de validar cuando se clickee en enviar
});

formulario.addEventListener('submit', (e) => {
    //e.preventDefault(); //evita enviar los datos

    if (campos.descripcion && campos.precio) {
        alert("Datos cargados correctamente");
    }
    else {
        alert("Datos incorrectos");
    }

});


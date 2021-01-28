const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');

const expresiones = {
    usuario: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
    nombre: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    password: /^.{4,12}$/, // 4 a 12 digitos.
    correo: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
    telefono: /^\d{7,14}$/ // 7 a 14 numeros.
}

const campos = {
    name: false,
    surname: false,
    email: false
}

const validarForm = (e) => {

    switch (e.target.name) {

        case "name":
            validarCampo(expresiones.nombre, e.target, 'name');
            break;

        case "surname":
            validarCampo(expresiones.nombre, e.target, 'surname');
            break;

        case "email":
            validarCampo(expresiones.correo, e.target, 'email');
            break;
    }
}

const validarCampo = (expresion, input, campo) => {

    if (expresion.test(input.value)) {
        document.getElementById(campo).classList.remove("input-incorrect");
        document.getElementById(campo).classList.add("input-correct");
        campos[campo] = true;
    }
    else {
        document.getElementById(campo).classList.add("input-incorrect");
        campos[campo] = false;
    }
}

inputs.forEach((input) => {
    input.addEventListener('keyup', validarForm); //key up ejecuta la función de validar cuando se suelta una tecla
    input.addEventListener('blur', validarForm); //blur ejecuta la función de validar cuando se clickee fuera del input
});

formulario.addEventListener('submit', (e) => {
    e.preventDefault(); //previene enviar los datos

    if (campos.name && campos.surname && campos.email) {
        formulario.reset();
        document.getElementById("form-message").classList.remove('form-message-active');
        document.getElementById("form-message").classList.add('form-message');
    }
    else {
        document.getElementById("form-message").classList.add('form-message-active');
    }
});
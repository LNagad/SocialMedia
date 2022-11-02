document.querySelectorAll('.nav-link').forEach(link => {
    if (link.href === window.location.href) {
        link.setAttribute('aria-current', 'page')
    }
    //aria - current="page"
})

const input = document.querySelector('#postOpen');
const modal = document.querySelector('#exampleModal');

input.addEventListener('click', updateValue);



function updateValue(e) {

    console.log("hola")
    
    $("#exampleModal").modal('show');
}
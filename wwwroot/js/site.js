// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#filter_dogs").on('change', function(value) {
    var uri = `api/filterDogsByOwner/${value.target.value}`
    fetch(uri, {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then((response) => {
            console.log('response result: ', response);
            var html = "";
            response.forEach(d => html += '<li>' + d.name + '</li>');
            console.log('html', html);
            $("#filterDogsResults").html(html);
        })
        .catch(error => console.error('Unable to add item.', error));
});

$("#get_owner").on('change', function(value) {
    var uri = `api/getOwnerByDog/${value.target.value}`
    fetch(uri, {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then((response) => {
            console.log('response result: ', response);
            var html = "";
            response.forEach(d => html += '<li>' + d.owner.name + ' owns ' + d.dog.name + '</li>');
            console.log('html', html);
            $("#getOwnerResult").html(html);
        })
        .catch(error => console.error('Unable to add item.', error));
});

$("#projection_select").on('change', function(value) {
    var uri = `api/getDogNamesAndTypes/${value.target.value}`
    fetch(uri, {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then((response) => {
            console.log('response result: ', response);
            var html = "";
            response.forEach(d => html += '<li>' + d.dogName + ' is a ' + d.dogType + '</li>');
            console.log('html', html);
            $("#getDogNamesAndTypes").html(html);
        })
        .catch(error => console.error('Unable to add item.', error));
});

$("#grouping_select").on('change', function(value) {
    var uri = `api/groupByParam/${value.target.value}`
    fetch(uri, {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then((response) => {
            console.log('response result: ', response);
            var html = "";
            var key = value.target.value;
            response.forEach((d) => {
                html += '<li>' + key + ' ';
                html += d.key;   
                html += ' - ';
                
                d.dogs.forEach((dog, index) => { 
                    html += dog;
                    if(index < (d.dogs.length -1)) {
                        html += ' & ';
                    }
                })

                html +=  '</li>';
            });
            console.log('html', html);
            $("#groupingResults").html(html);
        })
        .catch(error => console.error('Unable to add item.', error));
});

$("#quantifier_select").on('change', function(value) {
    var uri = `api/doesDogHaveAge/${value.target.value}`
    fetch(uri, {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then((response) => {
            var html = "";
            if(response) {
                html = "There is at least one dog that is " + value.target.value + " year(s) old!";
            } else {
                html = "No dogs are " + value.target.value + " year(s) old.";

            }
            $("#quantifierResults").html(html);
        })
        .catch(error => console.error('Unable to add item.', error));
});
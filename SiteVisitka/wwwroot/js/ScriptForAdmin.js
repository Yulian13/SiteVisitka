function getElement() {
	const form = document.forms["Test"];
	const name = form.elements["Name"];
	const description = form.elements["Description"];
	const address = form.elements["Address"];
	const text = form.elements["urlImages"];

	//GetWorks(name, description, address, text);
	CreatWork(name,description,address,text);
}

async function CreatWork(name, description, address, tex) {

	var path = '/api/Work';
	var work = {
		Name: name.value,
		Description: description.value,
		Address: address.value,
		urlImages: tex.value
	};
	
	var obj1 = JSON.stringify(work);
	
	const respons = await fetch(path, {
		method: "POST",
		headers: { "Accept": "application/json", "Content-Type": "application/json" },
		body: obj1
	});
	
	var e =1;
}

async function GetWorks(name, description, address, tex) {
	$.ajax({
		url: '/api/Work',
		type: 'GET',
		dataType: 'json',
		success: function (data) {
			console.log(data);
		},
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
	});
}
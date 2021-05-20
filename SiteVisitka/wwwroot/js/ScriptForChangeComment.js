async function deleteCom(Id) {
	var elems = GetElementsForDelete(Id);
	if(elems.checkBox.checked == false){
		return;
	}

	var path = '/api/Comment/' + Id;
	const respons = await fetch(path, {
			method: "DELETE",
			headers: { "Accept": "application/json" },
		}
	);

	if (respons.ok === true) {
		elems.divStatus.textContent = "Комментарий удален";
		elems.button.disabled = true;
		elems.checkBox.disabled = true;
	}
	else {
		const errorData = await respons.json();
		console.log("errors", errorData);
		elems.divStatus.textContent = "Ошибка";
	}
}

async function ApprovedCom(Id){
	var elems = GetElementsForPut(Id);

	var com = {
		Id: Id,
		Name: "PUT",
		Text: "Putting"

	};
	var obj = JSON.stringify(com);
	var path = '/api/Comment';
	
	const respons = await fetch(path, {
			method: "PUT",
			headers: { "Accept": "application/json", "Content-Type": "application/json" },
			body: obj
		}
	);

	if (respons.ok === true) {
		elems.divStatus.textContent = "Комментарий одобрен";
		elems.button.disabled = true;
	}
	else {
		const errorData = await respons.json();
		console.log("errors", errorData);
		elems.divStatus.textContent = "Ошибка";
	}
}

function GetElementsForPut(Id){
	var form = document.forms["comment" + Id];
	var divStatus = document.getElementById("status" + Id);
	var button = document.getElementById("butApp" + Id);

	return {
		divStatus,
		button	
	}
}

function GetElementsForDelete(Id){
	var form = document.forms["comment" + Id];
	var divStatus = document.getElementById("status" + Id);
	var checkBox = form.elements["exactlyDel"];
	var button = document.getElementById("butDel" + Id);

	return {
		divStatus,
		checkBox,
		button	
	}
}
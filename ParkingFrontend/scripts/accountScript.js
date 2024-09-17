document.addEventListener('DOMContentLoaded', async () => {
    reloadTable();

    newAccountBtn = document.getElementById('add-an-account');
    newAccountBtn.onclick = () => {
        const addAccountModal = new bootstrap.Modal(document.getElementById('addAccountModal'));
        addAccountModal.show();
        createNewAccount();
    };

});
function updateAccountsTable(accounts) {
    const tableBody = document.querySelector('#accounts-table tbody');
    tableBody.innerHTML = '';

    accounts.forEach(account => {
        const row = document.createElement('tr');

        const idCell = document.createElement('td');
        idCell.textContent = account.id 
        row.appendChild(idCell);

        const nameCell = document.createElement('td');
        nameCell.textContent = account.familyName; 
        row.appendChild(nameCell);

        const emailCell = document.createElement('td');
        emailCell.textContent = account.email; 
        row.appendChild(emailCell);
        
        const phoneCell = document.createElement('td');
        phoneCell.textContent = account.phone; 
        row.appendChild(phoneCell);
        
        const residentsCell = document.createElement('td');
        residentsCell.className = "residents-list";

        const actionsCell = document.createElement('td');
        actionsCell.className = 'action-buttons';

        const editButton = document.createElement('button');
        editButton.setAttribute("class", "btn btn-discovery rounded-3 btn-sm edit-btn");
        editButton.innerHTML='<i class="bi bi-pencil-square"></i> Edit'
        editButton.setAttribute("style", "margin-right: 5px;");
       
        editButton.onclick = () => {
            var accountModal = new bootstrap.Modal(document.getElementById('accountModal'));
            document.getElementById('family-name').value = account.familyName;
            document.getElementById('contact-email').value = account.email;
            document.getElementById('contact-phone').value = account.phone;
            populateResidentsDropdown(account.residents.$values, account);            
            populateVehiclesDropdown(account.vehicles.$values, account);
            accountModal.show();
        };
        
        actionsCell.appendChild(editButton);

        const deleteButton = document.createElement('button');
        deleteButton.setAttribute("class", "btn btn-danger rounded-3 btn-sm");
        deleteButton.innerHTML='<i class="bi bi-trash"></i> Delete'

        deleteButton.onclick = () => {
            if (confirm(`Are you sure you want to delete account ID ${account.id}?`)) {
                deleteRow(`https://localhost:5001/api/account/${account.id}`);
            }
        };
        actionsCell.appendChild(deleteButton);

        row.appendChild(actionsCell);

        tableBody.appendChild(row);
    });
}

async function reloadTable() {
    try {
        const response = await fetch('https://localhost:5001/api/account');
        if (!response.ok) {
            throw new Error(`Network response was ${response.status}`);
        }
        const result = await response.json();
        accounts = result.$values; 
        updateAccountsTable(accounts);
    } catch (error) {
        console.error('Error fetching accounts.', error);
    }
}

function createNewAccount()
{
    const saveButton = document.getElementById('save-add-account');
    saveButton.onclick = () => {
        const newFamilyName = document.getElementById('new-family-name').value;
        const newEmail = document.getElementById('new-email').value;
        const newPhoneNumber = document.getElementById('new-phone-number').value;
        const newId = document.getElementById('account-id').value;
        const newAccount = { id: newId, familyName: newFamilyName, email: newEmail, phone: newPhoneNumber};
        insertRow(newAccount, 'https://localhost:5001/api/account');
        bootstrap.Modal.getInstance(document.getElementById('addAccountModal')).hide();
    };
}

function saveResidentChanges(accountId, isNew, dropdownMenu)
{
    const newFirstName = document.getElementById('first-name').value;
    const newLastName = document.getElementById('last-name').value;
    const newAltPhone = document.getElementById('alternate-phone').value;
    const newId = document.getElementById('resident-id').value;
    const newResident = { id: newId, firstName: newFirstName, lastName: newLastName, altPhoneNumber: newAltPhone, accountId: accountId};
    if(isNew == true){
        insertRow(newResident, 'https://localhost:5001/api/resident');
        addNewResidentToList(newResident, dropdownMenu, accountId);
    }
    else
    {
        updateRow(newResident,`https://localhost:5001/api/resident/${newId}`);
        reloadTable();
    }
    bootstrap.Modal.getInstance(document.getElementById('addResidentModal')).hide();
    bootstrap.Modal.getInstance(document.getElementById('accountModal')).show();
}

function saveVehicleChanges(accountId, isNew, dropdownMenu)
{
    const newMake = document.getElementById('make').value;
    const newModel = document.getElementById('model').value;
    const newColor = document.getElementById('color').value;
    const newLicense = document.getElementById('license-plate').value;
    const newId = document.getElementById('vehicle-id').value;
    const newVehicle = { id: newId, make: newMake, model: newModel, color: newColor, licensePlate: newLicense, accountId: accountId};
    if(isNew == true){
        insertRow(newVehicle, 'https://localhost:5001/api/vehicle');
        addNewVehicleToList(newVehicle, dropdownMenu, accountId);
    }
    else
    {
        updateRow(newVehicle,`https://localhost:5001/api/vehicle/${newId}`);
        reloadTable();
    }
    bootstrap.Modal.getInstance(document.getElementById('addVehicleModal')).hide();
    bootstrap.Modal.getInstance(document.getElementById('accountModal')).show();
}


function addNewResidentToList(resident, dropdownMenu, accountId)
{
    const listItem = document.createElement('li');
    const a = document.createElement('a');
    a.className = 'dropdown-item';
    a.textContent = `${resident.firstName} ${resident.lastName}`;
    a.onclick = () => {
        const addResidentModal = new bootstrap.Modal(document.getElementById('addResidentModal'));
        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        const saveButton = document.getElementById('save-add-resident');
        const deleteSection = document.getElementById('delete-resident-section');
        const deleteButton = document.getElementById('delete-resident');

        document.getElementById('first-name').value = resident.firstName;
        document.getElementById('last-name').value = resident.lastName;
        document.getElementById('alternate-phone').value = resident.altPhoneNumber;
        document.getElementById('resident-id').value = resident.id;
        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        addResidentModal.show();

        deleteSection.style.display = 'block';

        saveButton.onclick = () => {
            saveResidentChanges(accountId, false);
         }
         deleteButton.onclick = () => {
            deleteRow(`https://localhost:5001/api/resident/${resident.id}`);
            deleteSection.style.display = 'none';
            addResidentModal.hide();
         }
    }
    a.dataset.id = resident.id; 
    listItem.appendChild(a);
    dropdownMenu.prepend(listItem);
}

function addNewVehicleToList(vehicle, dropdownMenu, accountId)
{
    const listItem = document.createElement('li');
    const a = document.createElement('a');
    a.className = 'dropdown-item';
    a.textContent = `${vehicle.make} ${vehicle.model}`;
    a.onclick = () => {
        const addVehicleModal = new bootstrap.Modal(document.getElementById('addVehicleModal'));
        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        const saveButton = document.getElementById('save-add-vehicle');
        const deleteSection = document.getElementById('delete-vehicle-section');
        const deleteButton = document.getElementById('delete-vehicle');

        document.getElementById('make').value = vehicle.make;
        document.getElementById('model').value = vehicle.model;
        document.getElementById('color').value = vehicle.color;
        document.getElementById('license-plate').value = vehicle.licensePlate;
        document.getElementById('vehicle-id').value = vehicle.id;

        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        addVehicleModal.show();
        deleteSection.style.display = 'block';

        saveButton.onclick = () => {
            saveVehicleChanges(accountId, false);
         }
         deleteButton.onclick = () => {
            deleteRow(`https://localhost:5001/api/vehicle/${vehicle.id}`);
            deleteSection.style.display = 'none';
            addVehicleModal.hide();
         }
    }
    a.dataset.id = vehicle.id; 
    listItem.appendChild(a);
    dropdownMenu.prepend(listItem);       
}

function populateResidentsDropdown(residents, account) {
    const dropdownMenu = document.getElementById('residents-dropdown');
    const saveButton = document.getElementById('save-add-resident');
    
    // Remove all children except the last two
    while (dropdownMenu.children.length > 2) {
        dropdownMenu.removeChild(dropdownMenu.firstChild);
    }
    
    residents.forEach((resident, index) => {
        addNewResidentToList(resident, dropdownMenu, account.id);
    });

    dropdownMenu.lastElementChild.addEventListener('click', (event) => {
        event.preventDefault();
        const addResidentModal = new bootstrap.Modal(document.getElementById('addResidentModal'));
        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        addResidentModal.show();
    });

    saveButton.onclick = () => {
       saveResidentChanges(account.id, true);
    }
}

function populateVehiclesDropdown(vehicles, account) {
    const dropdownMenu = document.getElementById('vehicles-dropdown');
    const saveButton = document.getElementById('save-add-vehicle');

    // Remove all children except the last two
    while (dropdownMenu.children.length > 2) {
    dropdownMenu.removeChild(dropdownMenu.firstChild);
    }
    vehicles.forEach((vehicle, index) => {
       addNewVehicleToList(vehicle, dropdownMenu, account.id);
    });

    dropdownMenu.lastElementChild.addEventListener('click', (event) => {
        event.preventDefault();
        const addVehicleModal = new bootstrap.Modal(document.getElementById('addVehicleModal'));
        bootstrap.Modal.getInstance(document.getElementById('accountModal')).hide();
        addVehicleModal.show();
    });

    saveButton.onclick = () => {
        saveResidentChanges(account.id, true);
    }
}

async function insertRow(myObject, url) {
    try {
        const response = await fetch(url, {
            method: 'POST', 
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myObject)
        });

        if (!response.ok) {
            console.log(JSON.stringify(myObject));
            throw new Error(`Network response was ${response.status}`);
        }
        else {
            const result = await response.json();
            reloadTable();
        }
    } catch (error) {
        console.error('Error with POST request.', error);
    }
}

async function deleteRow(url) {
    try {
        const response = await fetch(url, {method: 'DELETE'});
        if (!response.ok) {
            throw new Error(`Network response was ${response.status}`);
        }
        else
        {
            reloadTable();
        }

    } catch (error) {
        console.error('Error with DELETE request.', error);
    }
}

async function updateRow(myObject, url) {
    try {
        const response = await fetch(url, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myObject)
        });

        if (!response.ok) {
            throw new Error(`Network response was ${response.status}`);
        }
        else {
            const result = await response.json();
            reloadTable();
        }
    } catch (error) {
        console.error('Error with PUT request.', error);
    }
}

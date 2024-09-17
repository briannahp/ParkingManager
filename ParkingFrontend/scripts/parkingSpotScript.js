document.addEventListener('DOMContentLoaded', async () => {
    reloadTable();

    newParkingBtn = document.getElementById('add-new-parking');
    newParkingBtn.onclick = () => {
        const editParkingModal = new bootstrap.Modal(document.getElementById('editParkingModal'));
        editParkingModal.show();
        createOrUpdateParkingSpace();
    };
});

function createOrUpdateParkingSpace(parkingSpace)
{
    const saveButton = document.getElementById('save-edit-parking');
    const toggleDetails = document.getElementById('is-taken');
    const vehicleDetails = document.getElementById('vehicle-details');

    if(toggleDetails.checked)
    {
        vehicleDetails.style.display = 'block';
    }
    else {
        vehicleDetails.style.display = 'none';
    }
    toggleDetails.addEventListener('change', function() {
        if (toggleDetails.checked) {
            vehicleDetails.style.display = 'block';
        } else {
            vehicleDetails.style.display = 'none';
        }
    });
        
    saveButton.onclick = () => {
        const newParkingSpaceId = document.getElementById('parking-space-id').value;
        const newIsTaken = document.getElementById('is-taken').checked;
        if(newIsTaken == false)
        {
            const newParkingSpace = { id: newParkingSpaceId, isTaken: newIsTaken};
            if(parkingSpace == null) {
                insertNewParkingSpace(newParkingSpace);
             }
             else {
                 updateParkingSpace(newParkingSpace);
             }
        }
        else
        {
            const newVehicleId = document.getElementById('vehicle-id').value;
            const newVehicleMake = document.getElementById('vehicle-make').value;
            const newVehicleModel = document.getElementById('vehicle-model').value;
            const newVehicleColor = document.getElementById('vehicle-color').value;
            const newVehicleLicense = document.getElementById('vehicle-license').value;
            const newAccountId = document.getElementById('account-id').value;
            const newVehicle = {id: newVehicleId, make: newVehicleMake, model: newVehicleModel, color: newVehicleColor, licensePlate: newVehicleLicense, spotId: newParkingSpaceId, accountId: newAccountId};
            const newParkingSpace = { id: newParkingSpaceId, isTaken: newIsTaken, vehicleId: newVehicleId, vehicle: newVehicle};
            if(parkingSpace == null) {
               insertNewParkingSpace(newParkingSpace);
               reloadTable();
            }
            else {
                updateParkingSpace(newParkingSpace);
                reloadTable();
            }
        }
        bootstrap.Modal.getInstance(document.getElementById('editParkingModal')).hide();
        }
    }

function updateParkingTable(parkingSpaces) {
    const tableBody = document.querySelector('#parking-table tbody');
    tableBody.innerHTML = '';

    parkingSpaces.forEach(parkingSpace => {
        const row = document.createElement('tr');

        const idCell = document.createElement('td');
        idCell.textContent = parkingSpace.id;
        row.appendChild(idCell);

        const isTakenCell = document.createElement('td');
        var occupiedCheckBox = document.createElement('input');
        occupiedCheckBox.type = 'checkbox';
        occupiedCheckBox.checked = parkingSpace.isTaken;
        occupiedCheckBox.readOnly = true;
        isTakenCell.appendChild(occupiedCheckBox);
        row.appendChild(isTakenCell);

        const vehicleIdCell = document.createElement('td');
        vehicleIdCell.textContent = "";

        const vehicleCell = document.createElement('td');
        vehicleCell.textContent = "";

        if(parkingSpace.isTaken == true)
        {
            vehicleIdCell.textContent = parkingSpace.vehicleId;
            vehicleCell.textContent = `${parkingSpace.vehicle.make} ${parkingSpace.vehicle.model}`;
        }

        row.appendChild(vehicleIdCell);
        row.appendChild(vehicleCell);

        const actionsCell = document.createElement('td');
        actionsCell.className = 'action-buttons';

        const editButton = document.createElement('button');
        editButton.setAttribute("class", "btn btn-discovery rounded-3 btn-sm edit-btn");
        editButton.innerHTML='<i class="bi bi-pencil-square"></i> Edit'
        editButton.setAttribute("style", "margin-right: 5px;");
       
        editButton.onclick = () => {
            var editParkingModal = new bootstrap.Modal(document.getElementById('editParkingModal'));
            document.getElementById('parking-space-id').value = parkingSpace.id;
            document.getElementById('is-taken').checked = parkingSpace.isTaken;
            if(parkingSpace.isTaken == true)
            {
                document.getElementById('vehicle-id').value = parkingSpace.vehicleId;
                document.getElementById('vehicle-make').value = parkingSpace.vehicle.make;
                document.getElementById('vehicle-model').value = parkingSpace.vehicle.model;
                document.getElementById('vehicle-color').value = parkingSpace.vehicle.color;
                document.getElementById('vehicle-license').value = parkingSpace.vehicle.licensePlate;
                document.getElementById('account-id').value = parkingSpace.vehicle.accountId;
            }
            editParkingModal.show();
            createOrUpdateParkingSpace(parkingSpace);
        };
        actionsCell.appendChild(editButton);

        const deleteButton = document.createElement('button');
        deleteButton.setAttribute("class", "btn btn-danger rounded-3 btn-sm");
        deleteButton.innerHTML='<i class="bi bi-trash"></i> Delete'

        deleteButton.onclick = () => {
            if (confirm(`Are you sure you want to delete parking space #${parkingSpace.id}?`)) {
                deleteParkingSpace(`https://localhost:5001/api/parkingspace/${parkingSpace.id}`);
            }
        };
        actionsCell.appendChild(deleteButton);

        row.appendChild(actionsCell);
        tableBody.appendChild(row);
    });
}

async function reloadTable() {
    try {
        const response = await fetch('https://localhost:5001/api/parkingspace');
        if (!response.ok) {
            throw new Error(`Network response was ${response.status}`);
        }
        const result = await response.json();
        parkingSpaces = result.$values; 
        updateParkingTable(parkingSpaces);
    } catch (error) {
        console.error('Error fetching parking spaces.', error);
    }
}

async function deleteParkingSpace(url) {
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
        console.error('Error Deleting Parking Space.', error);
    }
}

async function updateParkingSpace(parkingSpace) {
    try {
        const response = await fetch(`https://localhost:5001/api/parkingspace/${parkingSpace.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(parkingSpace)
        });

        if (!response.ok) {
            console.log(JSON.stringify(parkingSpace));
            throw new Error(`Network response was ${response.status}`);
        }
        else {
            const result = await response.json();
        }
    } catch (error) {
        console.error(`Error updating existing parking space at ID ${parkingSpace.id}.`, error);
        reloadTable();
    }
}

async function insertNewParkingSpace (parkingSpace)
{
    try {
        const response = await fetch('https://localhost:5001/api/parkingspace', {
            method: 'POST', 
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(parkingSpace)
        });

        if (!response.ok) {
            console.log(JSON.stringify(parkingSpace));
            throw new Error(`Network response was ${response.status}`);
        }
        else {
            const result = await response.json();
        }
    } catch (error) {
        console.error('Error inserting a new parking space.', error);
        reloadTable();
    }
}


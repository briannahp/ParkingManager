document.addEventListener("DOMContentLoaded", () => {
    const apiUrl = "https://localhost:5001/api/account"; 

    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            const accountsDiv = document.getElementById("parking-accounts");
            let html = '<ul class="list-group">';

            data.forEach(account => {
                html += `<li class="list-group-item">
                            <strong>${account.familyName}</strong> - Residents: ${account.residents.length}
                         </li>`;
            });

            html += '</ul>';
            accountsDiv.innerHTML = html;
        })
        .catch(error => console.error("Error fetching data:", error));
});

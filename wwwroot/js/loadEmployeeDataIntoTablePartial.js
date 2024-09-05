const ALL_EMPLOYEES_ROUTE = 'http://localhost:5000/employees/all'
const GENERATE_TABLE_ROUTE = 'http://localhost:5000/Dashboard/GenerateTable'

document.addEventListener('DOMContentLoaded', () => 
{
    fetch(ALL_EMPLOYEES_ROUTE)
        .then((response) => response.json())
        .then((data) => fetch(
            GENERATE_TABLE_ROUTE, 
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            }
        ))
        .then((response) => response.text())
        .then((html) => 
        {
            document.querySelector('#loading').style.display = 'none';
            document.querySelector('#table').innerHTML = html;
        })
        .catch((error) => console.error('Error fetching employee data:', error))
})


export async function GetCars(pageNumber = 1, pageSize = 100, sort = "") {
    try {
        const response = await fetch('api/Cars/getcars', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'

        })

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse.$values;
    }
    catch (error) {
        console.error('Error occurred while fetching Cars:', error.message);
        throw error;
    }
}

export async function GetCarDetails(id) {
    console.log(id);
    try {
        const response = await fetch(`api/Cars/getcardetails?id=${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse.$values;
    }
    catch (error) {
        console.error('Error occurred while fetching Car Details:', error.message);
        throw error;
    }
}
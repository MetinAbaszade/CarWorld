import { useSelector } from "react-redux";


export async function GetTransmissions(languageid) {
    try {
        const response = await fetch(`api/Transmission/gettransmissions/${languageid}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse.$values;
    }
    catch (error) {
        console.error('Error occurred while fetching Transmissions:', error.message);
        throw error;
    }
}
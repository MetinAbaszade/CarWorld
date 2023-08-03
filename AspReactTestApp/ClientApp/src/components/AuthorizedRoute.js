import { useEffect, useState } from "react";
import { AuthService } from "../Services";
import { useNavigate } from "react-router-dom";


export default function AuthorizedRoute({ children }) {
    const [isAuthorized, setIsAuthorized] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetchData();
    }, []);

    useEffect(() => {
        if (isAuthorized === false) {
            navigate("/auth/login");
        }
    }, [isAuthorized]);

    async function fetchData() {
        // The actual check can vary. Maybe call an API, or check a value in localStorage.
        try {
            var result = await AuthService.CheckIsUserAuthenticated();
            if (result.status === 401) {
                setIsAuthorized(false);
                return;
            }
            setIsAuthorized(true);
        } catch (error) {
            setIsAuthorized(false);
        }
    }

    if (isAuthorized === true) {
        return (
            <>
                {children}
            </>
        );
    }

    return null; // Or a loading spinner
}
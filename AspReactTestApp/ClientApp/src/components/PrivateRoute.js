import React from 'react'
import { Navigate, useLocation } from 'react-router-dom'; 
import { AuthService } from '../Services';

export default function PrivateRoute({children}) {
    var user = AuthService.getUser();
    const location = useLocation()
    if(!user){
        return <Navigate to='/auth/login' state={{
            return_url: location.pathname
        }}/>
    }

  return children
}

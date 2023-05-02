import React from 'react'
import { AuthService } from '../Services';

export default function Profile() {
  const user = AuthService.getUser();
  return (
    <>
      <div>{user.userName}</div>
      <div>{user.password}</div>
    </>
  )
}

import React from 'react'
import { Link } from 'react-router-dom'

export default function PostNotFound() {
  return (
    <div>
        <p>Post not found</p>
        <Link to='/posts'>Return Posts</Link>
    </div>
  )
}

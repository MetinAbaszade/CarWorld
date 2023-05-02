import React from 'react'
import { Link } from 'react-router-dom'

export default function Posts() {
  return (
    <div>
        <ul>
            <li><Link to='/posts/as/01'>Post 1</Link></li>
            <li><Link to='/posts/fs/02'>Post 2</Link></li>
            <li><Link to='/posts/hg/03'>Post 3</Link></li>
        </ul>
    </div>
  )
}

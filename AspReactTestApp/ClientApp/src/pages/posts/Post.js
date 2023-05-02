import React from 'react'
import { useParams } from 'react-router-dom'

export default function Post() {
    const {url, id} = useParams()
  return (
    <div>Post {url} {id}</div>
  )
}

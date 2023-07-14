import { useEffect } from "react"
import { FueltypeService } from "../Services"

export default function Home() {
  
  async function FetcDatas() {
    var result = await FueltypeService.GetFueltypes();
  }

  useEffect(() => {
    FetcDatas();
  })

  return (
    'Home Page'
  )
}

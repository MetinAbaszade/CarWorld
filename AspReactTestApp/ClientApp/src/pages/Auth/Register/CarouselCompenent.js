import React from 'react'
import { Carousel } from 'react-responsive-carousel';


export default function CarouselCompenent() {
  return (
      <Carousel
          showThumbs={false}
          showArrows={false}
          showStatus={false}
          showIndicators={false}
          autoPlay={true}
          interval={3000}
          infiniteLoop={true}
          className="h-100"
      >
          <div className="h-100">
              <img src="/CarImages/Car1.jpg" className="h-100" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car2.jpg" className="h-100" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car3.jpg" className="h-100" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car4.jpg" className="h-100" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car5.jpg" className="h-100" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car6.jpg" className="h-100" />
          </div>
      </Carousel>
  )
}

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
              <img src="/CarImages/Car1.jpg" alt='Car1' className="h-100 object-cover object-bottom" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car2.jpg" alt='Car2' className="h-100 object-cover object-bottom" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car3.jpg" alt='Car3' className="h-100 object-cover object-bottom" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car4.jpg" alt='Car4' className="h-100 object-cover object-bottom" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car5.jpg" alt='Car5' className="h-100 object-cover object-bottom" />
          </div>
          <div className="h-100">
              <img src="/CarImages/Car6.jpg" alt='Car6' className="h-100 object-cover object-bottom" />
          </div>
      </Carousel>
  )
}

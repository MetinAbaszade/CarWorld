import React from 'react'
import './ProfileImageComponent.css'

export default function ProfileImageComponent({ setSelectedImage }) {
    const profileImg = useRef(null);

    const handleImageChange = (e) => {
        if (e.target.files && e.target.files[0]) {
            setSelectedImage(e.target.files[0]);
            const reader = new FileReader();
            reader.onload = function (e) {
                profileImg.current.src = e.target.result;
            };
            reader.readAsDataURL(e.target.files[0]);
        }
    };

  return (
      <div className="image-container">
          <img ref={profileImg} src="/UserLogo2.jpeg" alt="UserLogo" />
          <div className="upload-overlay" onClick={() => { document.getElementById('fileInput').click(); }}>
              <p className="upload-text">Upload Photo</p>
          </div>
          <input type="file" id="fileInput" accept="image/*" style={{ "display": "none" }} onChange={handleImageChange} />
      </div>
  )
}

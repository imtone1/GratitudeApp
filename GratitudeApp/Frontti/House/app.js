
let cursor =document.querySelector('.cursorFigure')
let house = document.querySelector('.house')



let x,y





document.addEventListener('mousemove',(e)=>{
    x=e.clientX
    y=e.clientY
})



house.addEventListener('mouseover', changeMouseOver)
house.addEventListener('mouseout', changeMouseOut,false)



function changeMouseOver(){
    house.style.backgroundImage = 'url(pngegg2.png)'
    // house.style.backgroundColor = 'red'
}

function changeMouseOut(){
    house.style.backgroundImage = 'url(pngegg.png)'

}



function putTheCursor(){
    cursor.style.top = y - cursor.getBoundingClientRect().height / 2 + 'px'
    cursor.style.left = x - cursor.getBoundingClientRect().width / 2 + 'px'
}

function repeatOfTen(){
    putTheCursor()
    requestAnimationFrame(repeatOfTen)
}


requestAnimationFrame(repeatOfTen)








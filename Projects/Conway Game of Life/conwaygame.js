
const container = document.querySelector('.container-fluid');
const head = document.querySelector("head");
const table = document.querySelector(".table");
const box = document.querySelector('.box');
// const enter = document.querySelector('.en')
const form = document.querySelector('form');
let numrow = parseInt(prompt('Enter number of rows: '));
let numcol = parseInt(prompt('Enter number of columns: '));


const createTable = function(numrow, numcol){
     head.innerHTML += '<style>.table{width: ' + (32*numcol)+ 'px;}</style>';
    for(let i = 0; i < numrow; i++){
        let strRow = '<div class="row row'+i +'"></div>';
        container.innerHTML += strRow;
    }
    for(let i = 0 ;i < numrow; i++){
        let selectRowStr = '.row'+i;
        let selectRow = document.querySelector(selectRowStr);
        for(let j = 0; j <numcol; j++){
            let strCol = '<div class="col-sm cell'+(i*numcol+j) +' dead"></div>';
            selectRow.innerHTML += strCol;
        }
    } 
}


createTable(numrow,numcol);
container.addEventListener('click', e => {
    if(e.target.classList.contains('col-sm')){
        e.target.classList.toggle('dead');
        e.target.classList.toggle('live'); 
    }
});

//generate next generation
const cellArray = document.querySelectorAll('.col-sm');
console.log(cellArray);
const mapLiveArray = new Array(cellArray.length);
const totalCell = numrow*numcol;
// for(let i  = 0; i < mapLiveArray.length; i++){
//     mapLiveArray[i] = 0;
// }
const increment = function () {
    for(let i = 0 ; i<cellArray.length; i++){
        let numLive = 0;
        if(i>=numcol      &&      i%numcol!=0      &&      cellArray[i-numcol-1].classList.contains('live')){ //top left
            numLive++;
        }
        if(i>= numcol      &&      cellArray[i-numcol].classList.contains('live')){ //top
            numLive++;
        }
        if(i>=numcol      &&      (i+1)%numcol!=0      &&      cellArray[i-numcol+1].classList.contains('live')){ //top right
            numLive++;
        }
        if(i%numcol!=0      &&      cellArray[i-1].classList.contains('live')){ //left
            numLive++;
        }
        if((i+1)%numcol!=0      &&      cellArray[i+1].classList.contains('live')){ //right
            numLive++;
        }
        if((i+numcol)<totalCell      &&      i%numcol!=0      &&      cellArray[i+numcol-1].classList.contains('live')){ //bottom left
            numLive++;
        }
        if((i+numcol)<totalCell      &&      cellArray[i+numcol].classList.contains('live')){ //bottom 
            numLive++;
        }
        if((i+numcol)<totalCell      &&      (i+1)%numcol!=0      &&      cellArray[i+numcol+1].classList.contains('live')){ //bottom right
            numLive++;
        }
        mapLiveArray[i] = numLive;
    }

    for(let i = 0 ; i<cellArray.length; i++){
        let currentCell = cellArray[i];
        if(currentCell.classList.contains('live') && (mapLiveArray[i]<2 || mapLiveArray[i]>3)){
            currentCell.classList.toggle('live');
            currentCell.classList.toggle('dead');
        }

        if(currentCell.classList.contains('dead') && mapLiveArray[i]==3){
            currentCell.classList.toggle('dead');
            currentCell.classList.toggle('live');
        }
    }

}


let timer;
let timerun=0;
const start = document.querySelector('.start');
start.addEventListener('click', () => {
    timer = setInterval(()=>{
        timerun += 1;
        box.innerHTML = timerun;
        increment();
    },500);
});

const stop = document.querySelector('.stop');
stop.addEventListener('click', () => {
    clearInterval(timer);
})

const inc1 = document.querySelector('.increment1');
inc1.addEventListener('click', ()=>{
    timerun += 1;
    box.innerHTML = timerun;
    increment();
});


const inc23 = document.querySelector('.increment23');
inc23.addEventListener('click', ()=>{
    let currGen = timerun;
    timer = setInterval(()=>{
        timerun +=1;
        if(timerun === currGen+23){
            clearInterval(timer);
        }
        box.innerHTML = timerun;
        increment();
    },500);
});

const reset = document.querySelector('.reset');
reset.addEventListener('click', ()=>{
    clearInterval(timer);
    timerun = 0;
    box.innerHTML = '';
    for(let i = 0 ; i<cellArray.length; i++){
        if(cellArray[i].classList.contains('live')){
            cellArray[i].classList.toggle('live');
            cellArray[i].classList.toggle('dead');
        }
    }
});


const pattern = document.querySelector('#pattern');
pattern.addEventListener('change', ()=>{
    let value = pattern.value;
    for(let i = 0 ; i<cellArray.length; i++){
        if(cellArray[i].classList.contains('live')){
            cellArray[i].classList.toggle('live');
            cellArray[i].classList.toggle('dead');
        }
    }
    if(value === 'boat'){
        let i = 3*numcol+5;
        cellArray[i].classList.toggle('live');
        cellArray[i].classList.toggle('dead');

        cellArray[i+1].classList.toggle('live');
        cellArray[i+1].classList.toggle('dead');

        cellArray[i+numcol].classList.toggle('live');
        cellArray[i+numcol].classList.toggle('dead');

        cellArray[i+numcol+2].classList.toggle('live');
        cellArray[i+numcol+2].classList.toggle('dead');

        cellArray[i+2*numcol+1].classList.toggle('live');
        cellArray[i+2*numcol+1].classList.toggle('dead');
    }

    if(value === 'beacon'){
        let i = 3*numcol+5;
        cellArray[i].classList.toggle('live');
        cellArray[i].classList.toggle('dead');

        cellArray[i+1].classList.toggle('live');
        cellArray[i+1].classList.toggle('dead');

        cellArray[i+numcol].classList.toggle('live');
        cellArray[i+numcol].classList.toggle('dead');

        cellArray[i+2*numcol+3].classList.toggle('live');
        cellArray[i+2*numcol+3].classList.toggle('dead');

        cellArray[i+3*numcol+2].classList.toggle('live');
        cellArray[i+3*numcol+2].classList.toggle('dead');

        cellArray[i+3*numcol+3].classList.toggle('live');
        cellArray[i+3*numcol+3].classList.toggle('dead');
    }

    if(value === 'toad'){
        let i = 3*numcol+5;
        cellArray[i].classList.toggle('live');
        cellArray[i].classList.toggle('dead');

        cellArray[i+1].classList.toggle('live');
        cellArray[i+1].classList.toggle('dead');

        cellArray[i+2].classList.toggle('live');
        cellArray[i+2].classList.toggle('dead');

        cellArray[i+numcol-1].classList.toggle('live');
        cellArray[i+numcol-1].classList.toggle('dead');

        cellArray[i+numcol].classList.toggle('live');
        cellArray[i+numcol].classList.toggle('dead');

        cellArray[i+numcol+1].classList.toggle('live');
        cellArray[i+numcol+1].classList.toggle('dead');

    }

    if(value === 'glider'){
        let i = 3*numcol+5;
        cellArray[i].classList.toggle('live');
        cellArray[i].classList.toggle('dead');

        cellArray[i+1].classList.toggle('live');
        cellArray[i+1].classList.toggle('dead');

        cellArray[i+2].classList.toggle('live');
        cellArray[i+2].classList.toggle('dead');

        cellArray[i-numcol+2].classList.toggle('live');
        cellArray[i-numcol+2].classList.toggle('dead');

        cellArray[i-2*numcol+1].classList.toggle('live');
        cellArray[i-2*numcol+1].classList.toggle('dead');

    }
    
});





const isSuccess = (e) => {
  let result = false;

  if(e){
    if(e.status === 200){
      result = true;
    }
  }

  return result;
};

const normalise = (value) => {
  const MAX = 100, MIN = 0;
  const result = (value - MIN) * 100 / (MAX - MIN);
  return result;
}

const getInitialFromWords = (words) => {
  let values = !words ? "" : words.trim().split(" ");
  if(values.length > 1){
      return (values[0][0] + values[1][0]).toUpperCase();
  }else if(values.length === 1){
      if(values[0].length > 1){
          return (values[0][0] + values[0][1]).toUpperCase();
      }else if (values[0].length === 1){
          return values[0][0].toUpperCase();
      }else{
          return "";
      }
  }else{
      return "";
  }
}

const formatDate = (dateValue) => {
  const dateObj = new Date(dateValue);
  const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
  ];
  const month = monthNames[dateObj.getMonth()];
  const day = dateObj.getDate();
  const year = dateObj.getFullYear();
  let hour = dateObj.getHours();
  hour = hour % 12;
  var ampm = (hour >= 12) ? 'AM' : 'PM';
  hour = (hour < 12) ? ((hour < 10) ? '0' + hour : hour) : ((hour === 12) ? '00' : hour);
  let minute = dateObj.getMinutes();
  minute = (minute < 10) ? '0' + minute : minute;
  const formattedDate = month + " " + day + ", " + year + " " + hour + ":" + minute + " " + ampm;
  return formattedDate;
}

const getFirstChar = (words) => {
  const checkedWords = undefinedToEmptyString(words);
  let res = checkedWords.trim().charAt(0).toUpperCase();
  return res;
}

const getPascalCaseString = (words) => {
  const checkedWords = undefinedToEmptyString(words);
  return checkedWords.replace(/\w+/g, (val) => {
      return val[0].toUpperCase() + val.slice(1).toLowerCase();
  });
}

const getFirstNameFromName = (words) => {
  const checkedWords = undefinedToEmptyString(words);
  let values = checkedWords.trim().split(",");
  if(values.length > 1){
      return getPascalCaseString(values[1].trim());
  }

  return "";
}

const getLastNameFromName = (words) => {
  const checkedWords = undefinedToEmptyString(words);
  let values = checkedWords.trim().split(",");
  if(values.length > 0){
      return getPascalCaseString(values[0].trim());
  }

  return "";
}

const undefinedToEmptyString = (e) => {
  if(e === undefined || e === null){
      return "";
  }

  return e;
}

const removeTrailingComma = (string) => {
  return string.trim().replace(/,\s*$/, "");
}

const wrapLongTextWithDots = (text, textWrapPosition) => {
  if(!text || text.length < 1) return '';
  if(text.length > textWrapPosition){
      let wrappedText = text.substring(0, textWrapPosition) + "...";
      return wrappedText;
  }else{
      return text;
  }
}

const getKeyByValue = (object, value) => {
  return Object.keys(object).find(key => object[key] === value);
}

const getConstantForValue = (constant, value) => {
  let result = '';

  if(constant && value){
    result = constant[value];
  }

  return result;
}

const todayDate = () => {
  var today = new Date();
  var dd = String(today.getDate()).padStart(2, '0');
  var mm = String(today.getMonth() + 1).padStart(2, '0'); 
  var yyyy = today.getFullYear();

  today = mm + '/' + dd + '/' + yyyy;

  return today;
}

const validDate = (dateString) => {
  if(dateString){
    const toCheck = new Date(dateString);
    return toCheck.getTime() === toCheck.getTime();
  }

  return false;
}

const lookupFilteredByType = (lookups, type) => {
  let result = [];

  if(lookups && type){
    lookups.filter(value => value.type === type)[0].value.map(item => {
      result.push({id:item.id, name:item.name});
    });
  }

  return result;
}

const getRandomNumber = (min, max) => {
  if(!min){
    min = 1
  }

  if(!max){
    max = 9999;
  }

  return Math.floor(Math.random() * (max - min + 1)) + min;
}

const urlFixer = (url) => {
  let result = url;

  if(url){
    const val = url.trim();

    if(val){
      if(RegExp("^((http|https|ftp):\/\/)").test(val)){
        
      } else {
        result = 'http://' + val;
      }
    }
  }    

  return result
}

export default{
  isSuccess,
  normalise,
  getInitialFromWords,
  getFirstChar,
  getFirstNameFromName,
  getInitialFromWords,
  getKeyByValue,
  removeTrailingComma,
  wrapLongTextWithDots,
  getLastNameFromName,
  undefinedToEmptyString,
  getPascalCaseString,
  formatDate,
  todayDate,
  validDate,
  lookupFilteredByType,
  getConstantForValue,
  getRandomNumber,
  urlFixer
};
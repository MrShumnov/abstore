import "./checkbox.scss"
import React from "react";
import MyText from "../text";

type Props = {
    text: string,
    defaultChecked: boolean
    onChange: () => void,
}

export default function Checkbox(props: Props) {
  return (
    <div className="box-container">
      <input type="checkbox" defaultChecked={props.defaultChecked} onChange={props.onChange}/>
      <MyText>{props.text}</MyText>
    </div>
  );
}
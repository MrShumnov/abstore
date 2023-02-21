import "./filter-group.scss";
import React, { useState } from "react";
import MyText from "../text";
import Checkbox from "../checkbox/checkbox";
import { createSearchParams } from "react-router-dom";

type Box = {
    boxName: string,
    optionName: string,
    defaultValue: boolean
}

type Props = {
    title: string,
    boxes: Box[],

    addUrlParam: (value: any) => void;
}

export default function FilterGroup(props: Props) {
    const [checked, setChecked] = useState(props.boxes.map((val: any) => val.defaultValue));

    const GetOnChanged = (idx: number) => {
        return () => {
            checked[idx] = !checked[idx];
            setChecked(checked);

            let options = [];

            for (let i = 0; i < props.boxes.length; i++)
                if (checked[i])
                    options.push(props.boxes[i].optionName)

            props.addUrlParam(options);
        }
    }

    return (
        <div className="filter-checkboxes">
            <div>
                <MyText>{props.title}</MyText>
            </div>
            {props.boxes.map((box: Box, index: number) => (
                    <Checkbox key={index} text={box.boxName} defaultChecked={box.defaultValue} onChange={GetOnChanged(index)}/>
                ))
            }
        </div>
    )
}
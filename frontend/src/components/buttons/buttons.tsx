import React from 'react';
import './buttons.css'
import MyText from "../text";

interface Props {
    text: string,
    onClick: () => void,
    wrap?: boolean 
}

export function RectButton(props: Props) {
    const wrap = props.wrap ? "normal" : "nowrap";

    return (
        <button className='rect-button' onClick={props.onClick} style={{whiteSpace: wrap}}>
            <MyText>{ props.text }</MyText>
        </button>
    )
};

export function TextButton(props: Props) {
    return (
        <button className='text-button' onClick={props.onClick}>
            <MyText>{ props.text }</MyText>
        </button>
    )
};

export function RoundButton(props: any) {
    return (
        <button className='round-button' onClick={props.onClick}>
            { props.children }
        </button>
    )
};
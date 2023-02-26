import { useMediaQuery } from 'react-responsive';

export enum DeviceType {
    Mobile = 0,
    BigMobile = 1,
    Tablet = 2,
    Desktop = 3
}

export function useDeviceType() {
    const isMobile = useMediaQuery({
        query: "(max-width: 650px)"
    });

    const isBigMobile = useMediaQuery({
        query: "(max-width: 880px)"
    });

    const isTablet = useMediaQuery({
        query: "(max-width: 1200px)"
    });
    
    if (isMobile)
    return DeviceType.Mobile;
    
    if (isBigMobile)
        return DeviceType.BigMobile;

    if (isTablet)
        return DeviceType.Tablet;

    return DeviceType.Desktop;
};

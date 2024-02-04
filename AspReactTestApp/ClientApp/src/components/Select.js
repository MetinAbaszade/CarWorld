import PropTypes from 'prop-types';
import { useState, useMemo, useEffect } from 'react';
import { uuid } from './uuid';
import { classNames } from './classNames';
import './Select.css'

export const Select = ({ options, label, onChange }) => {
    const [value, setValue] = useState('');
    const [open, setOpen] = useState(false);

    const [id] = useState(uuid());

    useEffect(() => {
        function handleOutsideClick(e) {
            if (
                !e.target.closest(`#Toggle-${id}`) &&
                !e.target.closest(`#Select-${id}`)
            )
                setOpen(false);
        }

        document.addEventListener('mousedown', handleOutsideClick);

        return () => document.removeEventListener('mousedown', handleOutsideClick);
    }, [id]);

    const opt = useMemo(() => {
        // Manage search and options
        const OPTIONS = options.filter(
            (o) =>
                o.name.toString().toLowerCase().indexOf(value.toString().toLowerCase()) !== -1
        );

        return OPTIONS.length > 0 ? (
            OPTIONS.map((o, i) => (
                <div
                    id="options-content-wrapper"
                    key={i}
                    className='ps-3 py-1 cursor-pointer text-neutral-600 hover:bg-neutral-300'
                    onClick={() => {
                        onChange(o.id);
                        setValue(o.name);
                        setOpen(false);
                    }}
                >
                    {o.name}
                </div>
            ))
        ) : (
            <div
                key='not-found'
                className='px-3 py-1 cursor-pointer text-neutral-600 hover:bg-neutral-300'
                onClick={() => {
                    onChange('');
                    setOpen(false);
                }}
            >
                No Matches Found
            </div>
        );
    }, [options, value, onChange]);

    useMemo(() => setValue(value), [value]);

    return (
        <div id={`Select-${id}`} className='relative w-48 inline-flex flex-col items-center justify-center '>
            <div className='bg-zinc-100 relative flex p-2 border-2 border-red-500 rounded-md '>
                <label htmlFor={`input-${id}`} className=''>{label + (open || value ? ': ' : '')}</label>
                <input
                    id={`input-${id}`}
                    className='outline-none input-field ps-2 w-100 bg-zinc-100'
                    type='text'
                    value={value}
                    onChange={(e) => setValue(e.target.value)}
                    onFocus={() => { setOpen(true); setValue(''); }}
                />
                {/* <label htmlFor={`input-${id}`} className={`floating-label bg-zinc-100 ${value && 'active'}`}>{label}</label> */}

                <img onClick={() => setOpen((open) => !open)} src='/arrow svg.svg'  className={`transition-transform border-0 ${open ? 'rotate-0' : '-rotate-180'} `} />
            </div>
            <div
                id='options'
                className={classNames(
                    'w-48 border-neutral-400 rounded-md transition-all mt-2 bg-zinc-100',
                    open ? 'max-h-40 border' : 'max-h-0 border-0'
                )}
            >
                {opt}
            </div>
        </div>
    );
};

Select.propTypes = {
    options: PropTypes.array.isRequired,
    value: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
};

Select.defaultProps = {
    options: [],
    value: '',
    onChange: () => { },
};

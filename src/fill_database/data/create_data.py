from string import ascii_lowercase, ascii_uppercase

p = []

for i, l, u in zip(range(1, 53, 2), ascii_lowercase, ascii_uppercase):
    p.append([str(i), l, '0.99', '0', f'Letter №{i} of the alphabet!', '2023-02-10', '2023-02-10'])
    p.append([str(i + 1), u, '0.99', '0', f'Letter №{i} of the alphabet!', '2023-02-10', '2023-02-10'])

p.append(['53', ' ', '0', '0', 'Just a regular space', '2023-02-10', '2023-02-10'])
p.append(['54', '","', '1.99', '0', 'Indispensable comma!', '2023-02-10', '2023-02-10'])
p.append(['55', '.', '9.99', '0', 'Finish your thought with a full point!', '2023-02-10', '2023-02-10'])

with open(r'C:\Users\mrshu\reps\abstore\src\fill_database\data\products.csv', 'w') as f:
    for params in p:
        f.write(','.join(params) + '\n')
    

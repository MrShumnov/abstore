from string import ascii_lowercase, ascii_uppercase

p = []

for i, l, u in zip(range(26), ascii_lowercase, ascii_uppercase):
    p.append([l, '0.99', '0', f'"Letter №{i + 1} of the alphabet, lowercase, reasonably priced!"', '2023-02-10', '2023-02-10'])
    p.append([u, '0.99', '0', f'"Letter №{i + 1} of the alphabet, uppercase, reasonably priced!"', '2023-02-10', '2023-02-10'])

p.append([' ', '0', '0', 'Just a regular space', '2023-02-10', '2023-02-10'])
p.append(['","', '1.99', '0', 'Indispensable comma!', '2023-02-10', '2023-02-10'])
p.append(['.', '9.99', '0', 'Finish your thought with a full point!', '2023-02-10', '2023-02-10'])

with open(r'C:\Users\mrshu\reps\abstore\src\fill_database\data\products.csv', 'w') as f:
    for params in p:
        f.write(','.join(params) + '\n')
    

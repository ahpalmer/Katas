import csv
import os

def extract_date_and_amount(input_file, output_file):
    """
    Extract date and monetary value from CSV file.
    Assumes date is in first column and amount is in last column.
    """

    print("Current working directory:", os.getcwd())
    print("Files in current directory:", os.listdir('.'))
    with open(input_file, 'r', newline='', encoding='utf-8') as infile:
        with open(output_file, 'w', newline='', encoding='utf-8') as outfile:
            reader = csv.reader(infile)
            writer = csv.writer(outfile)
            
            # Write header for output file
            writer.writerow(['Date', 'Amount'])
            
            for row in reader:
                if len(row) >= 2:  # Make sure row has at least 2 columns
                    date = row[0].strip()  # First column
                    amount = row[-1].strip()  # Last column
                    writer.writerow([date, amount])

def extract_date_and_amount_print(input_file):
    """
    Extract and print date and monetary value from CSV file.
    Useful for testing/viewing results.
    """
    with open(input_file, 'r', newline='', encoding='utf-8') as infile:
        reader = csv.reader(infile)
        
        print("Date\t\tAmount")
        print("-" * 30)
        
        for row in reader:
            if len(row) >= 2:  # Make sure row has at least 2 columns
                date = row[0].strip()  # First column
                amount = row[-1].strip()  # Last column
                print(f"{date}\t\t{amount}")

# Example usage:
if __name__ == "__main__":
    input_filename = "input.csv"  # Replace with your input file name
    output_filename = "output.csv"  # Replace with desired output file name
    
    # Extract to new file
    extract_date_and_amount(input_filename, output_filename)
    print(f"Extracted data saved to {output_filename}")
    
    # Or just print the results
    print("\nPreview of extracted data:")
    extract_date_and_amount_print(input_filename)